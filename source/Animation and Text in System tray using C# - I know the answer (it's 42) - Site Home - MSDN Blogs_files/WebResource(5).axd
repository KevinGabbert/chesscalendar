﻿function Common_UserFriendship_Request(context, control, userId, friendId) {
    var message = '{"userIdString":"' + userId + '","friendIdString":"' + friendId + '"}';
	$.ajax({
		type: "POST",
		url: Common_UserFriendship_AjaxEndpoint + "/RequestFriendship",
		data: message,
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		beforeSend: function(xhr) {
		    TelligentUtility.WriteAuthorizationHeader(xhr);
		},
		success: function(response) {
		    if(response.d) {
			    $(control).hide("fast");
			}
		}
	});
}