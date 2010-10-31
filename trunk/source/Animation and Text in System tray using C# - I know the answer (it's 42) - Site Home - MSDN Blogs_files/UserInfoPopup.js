
function Core_UserInfoPopup_AttachToUserElements(context)
{
    $('.internal-link.view-user-profile, .internal-link.view-profile, .avatar > a')
        .live("mouseover", function() { Core_UserInfoPopup_OpenPopupTimeout(context, this); })
        .live("mouseout", function() { Core_UserInfoPopup_ClosePopup(context, this); });

    Core_UserInfoPopup_AttachToUserGeneratedContentElements(context);
}

function Core_UserInfoPopup_AttachToUserGeneratedContentElements(context)
{
    var urlPatternRe = Core_UserInfoPopup_GetAnchorUrlPattern(context);
    $('.user-defined-markup a').each(function() {
        var a = $(this).attr("core_userinfopopup");
        if (!a && urlPatternRe.test(this.href)) 
            $(this)
                .attr('core_userinfopopup', 'true')
                .mouseover(function() { Core_UserInfoPopup_OpenPopupTimeout(context, this); })
                .mouseout(function() { Core_UserInfoPopup_ClosePopup(context, this); });
    });

    window.setTimeout(function() { Core_UserInfoPopup_AttachToUserGeneratedContentElements(context); }, 999);
}

function Core_UserInfoPopup_OpenPopupTimeout(context, element)
{
    if (context.parameter.lastElement)
        return;

    context.parameter.lastElement = element;

    window.clearTimeout(context.parameter.popupTimeout);
    context.parameter.popupTimeout = window.setTimeout(function() { Core_UserInfoPopup_OpenPopup(context, element); }, 999);
}

function Core_UserInfoPopup_IgnoreElement(element)
{
    var parent = element.parentNode;
    while (parent)
    {
        if (parent.className && $(parent).hasClass('user-navigation'))
            return true;

        parent = parent.parentNode;
    }

    return false;
}

function Core_UserInfoPopup_OpenPopup(context, element)
{
    var elementInfo = Telligent_Common.GetElementInfo(element);
    var popup = eval(context.parameter.popupName);
    popup.SetPanelContent('');

    if (element.nodeName == 'A' && !Core_UserInfoPopup_IgnoreElement(element))
    {
        var userName = Core_UserInfoPopup_GetAnchorUrlPattern(context).exec(element.href)[1];
        
        var userInfo = Core_UserInfoPopup_GetCachedUserInfoByUsername(context, userName);
        if (userInfo)
        {
            Core_UserInfoPopup_PopulateUserInfo(context, userInfo);
            popup.Show(elementInfo.Left - 17, elementInfo.Top, elementInfo.Width, elementInfo.Height);
            
            return;
        }        
        
        var message = '{"username":"' + encodeURIComponent(userName) + '"}';

        $.ajax({
            type: "POST",
            url: Core_UserInfoPopup_AjaxEndpoint + "/GetUserInfoByUsername",
            data: message,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function(xhr) {
                TelligentUtility.WriteAuthorizationHeader(xhr);
            },
            success: function(msg)
            {
                Core_UserInfoPopup_AddToCache(context, msg.d);
                Core_UserInfoPopup_PopulateUserInfo(context, msg.d);
                popup.Show(elementInfo.Left - 17, elementInfo.Top, elementInfo.Width, elementInfo.Height);
            },
            error: function(XMLHttpRequest, textStatus, errorThrown)
            {
                // Do not show the error
            }
        });
    }
}

function Core_UserInfoPopup_PopulateUserInfo(context, userInfo)
{
    var popup = eval(context.parameter.popupName);
    var html = context.parameter.userInfoHtml
        .replace(/{DisplayName}/g, userInfo.DisplayName)
        .replace(/{PostRank}/g, userInfo.UserPostRankIcon);

    if (userInfo.ShowPostCount)
        html = html.replace(/{PostCountArea}/g, context.parameter.postCountAreaHtml.replace(/{PostCount}/g, userInfo.PostCount));
    else
        html = html.replace(/{PostCountArea}/g, '');
    
    if (userInfo.ShowUserRank)
        html = html.replace(/{UserRankArea}/g, context.parameter.userRankAreaHtml.replace(/{UserRank}/g, userInfo.UserRank));
    else
        html = html.replace(/{UserRankArea}/g, '');

    if (userInfo.ShowPointCount)
        html = html.replace(/{PointCountArea}/g, context.parameter.pointCountAreaHtml.replace(/{PointCount}/g, userInfo.PointCount));
    else
        html = html.replace(/{PointCountArea}/g, '');

    if (userInfo.RoleIcons.length > 0)
        html = html.replace(/{RoleIconArea}/g, context.parameter.roleIconAreaHtml.replace(/{RoleIcons}/g, '<img src="' + userInfo.RoleIcons.join('" /><img src="') + '" />'));
    else
        html = html.replace(/{RoleIconArea}/g, '');

    popup.SetPanelContent(html);

    Core_UserInfoPopup_EnsurePopupWidth(context);
}

function Core_UserInfoPopup_EnsurePopupWidth(context)
{
    window.clearTimeout(context.parameter.popupResizeHandle);

    var popup = eval(context.parameter.popupName);
    if (popup.IsShown())
    {
        if (Telligent_Common.IsIE())
        {
            // this is a little bit of a hack (since it uses internal members of popup panel), but it effectively resizes the panel to fit the content
            popup._panelMask.style.width = popup._panel.offsetWidth + 'px';
            popup._panelMask.style.height = popup._panel.offsetHeight + 'px';
        }
        else
            popup.Refresh();
    }

    context.parameter.popupResizeHandle = window.setTimeout(function() { Core_UserInfoPopup_EnsurePopupWidth(context); }, 249);
}

function Core_UserInfoPopup_ClosePopup(context, element)
{
    window.clearTimeout(context.parameter.popupTimeout);

    var popup = eval(context.parameter.popupName);
    popup.Hide();

    context.parameter.lastElement = null;
}

function Core_UserInfoPopup_GetAnchorUrlPattern(context)
{
    return new RegExp(context.parameter.userProfileUrlPattern, 'i');
}

function Core_UserInfoPopup_GetImageUrlPattern(context)
{
    return new RegExp(context.parameter.userAvatarUrlPattern, 'i');
}

function Core_UserInfoPopup_GetCachedUserInfoByUsername(context, username)
{
    if (context.parameter.cacheByUsername)
        return context.parameter.cacheByUsername[username];
    else
        return null;
}

function Core_UserInfoPopup_GetCachedUserInfoByUserId(context, userId)
{
    if (context.parameter.cacheByUserId)
        return context.parameter.cacheByUserId[userId];
    else
        return null;
}

function Core_UserInfoPopup_AddToCache(context, userInfo)
{
    if (!context.parameter.cacheByUsername)
        context.parameter.cacheByUsername = new Object();

    if (!context.parameter.cacheByUserId)
        context.parameter.cacheByUserId = new Object();

    context.parameter.cacheByUsername[userInfo.Username] = userInfo;
    context.parameter.cacheByUserId[userInfo.UserId] = userInfo;
}