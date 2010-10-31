//Executes the function when the DOM is ready to be used
$(document).ready(function() {

    $('a.iconsOnPanel').each(function(index) {
        var rawUrl = $(this).attr("href");
        rawUrl = rawUrl.replace("TITLE_REPLACE", encodeURI(document.title));
        $(this).attr("href", rawUrl);
    }
);

});

//
//jQuery(".ShareThisMainPanel").offset({ top: 40, left: 46 });

//ShowMoreLessButtonsPanel - div with two buttons More Less
function ShowMoreLessButtonsPanelOnClick(currentDIvid) {
       var slidinPanel = jQuery('.tierTwoPanel').css('display');
       if (slidinPanel == 'none') {
           jQuery(".tierTwoPanel").show();
       }
       else {
           jQuery(".tierTwoPanel").hide();
       }

       var buttonsMore = jQuery("#btn_more").css('display');
       if (buttonsMore == 'none') {
           jQuery("#btn_more").show();
       }
       else {
           jQuery("#btn_more").hide();
       }
       
       var buttonsLess = jQuery("#btn_less").css('display');
       if (buttonsLess == 'none') {
           jQuery("#btn_less").show();
       }
       else {
           jQuery("#btn_less").hide();
       }
    return;
}

//timer = 3000;
var mouseIn = false;


//ShowMoreLessButtonsPanel - div with two buttons More Less
function ShowMoreLessButtonsPanelOnmouseOver(thisid) {
    mouseIn = true;
    return;
}
function ShowMoreLessButtonsPanelOnmouseOut(thisid) {
    var slidinPanel = jQuery('.tierTwoPanel').css('display');
    if (slidinPanel == 'none') {
        //mouseIn = true;
    }
    else {
        if (mouseIn == false) 
        { 
            setTimeout(function() {
                ShowMoreLessButtonsPanelOnClick(thisid);
            }, 3000);
        }
    }

    return;
} 
 
//ShareThisChildRootPanel
function ShareThisChildRootPanelOnmouseOver(thisid) {
    mouseIn = true;
    return;
}
function ShareThisChildRootPanelOnmouseOut(thisid) {
    mouseIn = false;
    return;
}
//TierTwoPanel
function TierTwoPanelOnclick(thisid){ return;}

function TierTwoPanelOnmouseOver(thisid) {
    mouseIn = true;
    return;
}
function TierTwoPanelOnmouseOut(thisid) {
    mouseIn = false;
    return;
}