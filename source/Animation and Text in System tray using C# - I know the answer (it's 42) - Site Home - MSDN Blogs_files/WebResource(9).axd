
function Telligent_PopupPanel(varName,cssClass,position,zIndex,onPanelShowFunction,onPanelHideFunction,hideOnDocumentClick,initialContentId,onMouseOverFunction,onMouseOutFunction)
{this._variableName=varName;this.CssClass=cssClass;this.Position=position;this.OnPanelShowFunction=onPanelShowFunction;this.OnPanelHideFunction=onPanelHideFunction;this.OnMouseOverFunction=onMouseOverFunction;this.OnMouseOutFunction=onMouseOutFunction;this.ZIndex=zIndex;this._isShown=false;this._initialized=false;this._lastPosition=null;this._initialContentContainer=document.getElementById(initialContentId);this._panel=null;this._panelMask=null;this._animationHandle=null;this._originalDocumentOnClick=null;this._isOpening=false;this._checkForScrollResizeHandle=null;this._lastWindowInfo=null;this._hideOnDocumentClick=hideOnDocumentClick;this._panelMask=document.createElement('div');this._panelMask.style.position='absolute';this._panelMask.style.display='none';this._panel=document.createElement('div');this._panel.style.position='absolute';this._panel.style.display='none';this._panel.style.left='0px';this._panel.style.top='0px';this._panel.className=this.CssClass;this._panelMask.appendChild(this._panel);Telligent_Common.RegisterDisposableControl(this._variableName);}
Telligent_PopupPanel.prototype._onMouseOver=function(e)
{if(this.OnMouseOverFunction)
this.OnMouseOverFunction();}
Telligent_PopupPanel.prototype._onMouseOut=function(e)
{if(this.OnMouseOutFunction)
this.OnMouseOutFunction();}
Telligent_PopupPanel.prototype.IsShown=function()
{return this._isShown;}
Telligent_PopupPanel.prototype._registerDocumentOnClick=function()
{this._originalDocumentOnClick=document.onclick;document.onclick=new Function(this._variableName+'._documentOnClick();');}
Telligent_PopupPanel.prototype.ShowAtElement=function(element,ignoreHideAndAnimation)
{var elementInfo=Telligent_Common.GetElementInfo(element);this.Show(elementInfo.Left,elementInfo.Top,elementInfo.Width,elementInfo.Height,ignoreHideAndAnimation);}
Telligent_PopupPanel.prototype.Show=function(x,y,positionWidth,positionHeight,ignoreHideAndAnimation)
{if(!this._initialized)
this._initialize();if(!this._isShown&&this._hideOnDocumentClick)
window.setTimeout(new Function(this._variableName+'._registerDocumentOnClick();'),49);if(!ignoreHideAndAnimation&&this._isShown)
this.Hide();else if(this._hiddenSelects)
Telligent_Common.ShowSelectBoxes(this._panelMask);var originalX=x;var originalY=y;this._panelMask.style.position='absolute';this._panelMask.style.zIndex=this.ZIndex;this._panelMask.style.display='block';this._panelMask.style.visibility='hidden';this._panelMask.style.overflow='visible';this._panel.style.position='absolute';this._panel.style.display='block';this._panel.style.visibility='hidden';this._panel.className=this.CssClass;this._panel.style.left='0px';this._panel.style.top='0px';this._panelMask.style.width=this._panel.offsetWidth+'px';this._panelMask.style.height=this._panel.offsetHeight+'px';this._lastWindowInfo=Telligent_Common.GetWindowInfo();var panelWidth=this._panel.offsetWidth;var panelHeight=this._panel.offsetHeight;var animatePropertyName,animateTargetValue,animateNextValue;var iLeft=this.Position.indexOf('left');var iRight=this.Position.indexOf('right');var iUp=this.Position.indexOf('up');var iDown=this.Position.indexOf('down');if(iLeft==0||iRight==0)
{if(!(iLeft>-1&&iRight==-1)&&((iRight>-1&&iLeft==-1)||(this._lastWindowInfo.Width+this._lastWindowInfo.ScrollX)-(x+positionWidth)>x-this._lastWindowInfo.ScrollX||(x-this._lastWindowInfo.ScrollX)<panelWidth))
{this._panelMask.style.left=(x+positionWidth)+"px";animatePropertyName='style.left';animateTargetValue=0;animateNextValue=-panelWidth;}
else
{this._panelMask.style.left=(x-panelWidth)+"px";animatePropertyName='style.left';animateTargetValue=0;animateNextValue=panelWidth;}
if(!(iUp>-1&&iDown==-1)&&((iDown>-1&&iUp==-1)||(this._lastWindowInfo.Height+this._lastWindowInfo.ScrollY)-y>(y+positionHeight)-this._lastWindowInfo.ScrollY||(y+positionHeight)-this._lastWindowInfo.ScrollY<panelHeight))
{this._panelMask.style.top=y+"px";}
else
{this._panelMask.style.top=(y+positionHeight-panelHeight)+"px";}}
else
{if(!(iLeft>-1&&iRight==-1)&&((iRight>-1&&iLeft==-1)||(this._lastWindowInfo.Width+this._lastWindowInfo.ScrollX)-x>(x+positionWidth)-this._lastWindowInfo.ScrollX||(x+positionWidth)-this._lastWindowInfo.ScrollX<panelWidth))
{this._panelMask.style.left=x+"px";}
else
{this._panelMask.style.left=(x+positionWidth-panelWidth)+"px";}
if(!(iUp>-1&&iDown==-1)&&((iDown>-1&&iUp==-1)||(this._lastWindowInfo.Height+this._lastWindowInfo.ScrollY)-(y+positionHeight)>(y-this._lastWindowInfo.ScrollY)||(y-this._lastWindowInfo.ScrollY)<panelHeight))
{this._panelMask.style.top=(y+positionHeight)+"px";animatePropertyName='style.top';animateTargetValue=0;animateNextValue=-panelHeight;}
else
{this._panelMask.style.top=(y-panelHeight)+"px";animatePropertyName='style.top';animateTargetValue=0;animateNextValue=panelHeight;}}
this._panel.style.visibility='visible';this._panelMask.style.visibility='visible';this._panelMask.style.overflow='hidden';Telligent_Common.HideSelectBoxes(this._panelMask);this._isOpening=true;if(ignoreHideAndAnimation)
this._animationHandle=window.setTimeout(new Function(this._variableName+'._animate(\''+animatePropertyName+'\','+animateTargetValue+','+animateTargetValue+',0,0);'),9);else
this._animate(animatePropertyName,animateTargetValue,animateNextValue,animateNextValue>animateTargetValue?-((animateNextValue-animateTargetValue)/3):((animateTargetValue-animateNextValue)/3),.67);if(!this._isShown)
{this._isShown=true;this._lastPosition={X:originalX,Y:originalY,Width:positionWidth,Height:positionHeight};if(this.OnPanelShowFunction)
this.OnPanelShowFunction();}}
Telligent_PopupPanel.prototype._checkForScrollResize=function()
{if(this._checkForScrollResizeHandle)
window.clearTimeout(this._checkForScrollResizeHandle);if(this._isShown&&!this._isOpening&&this._lastWindowInfo)
{var windowInfo=Telligent_Common.GetWindowInfo();if(windowInfo.Width!=this._lastWindowInfo.Width||windowInfo.Height!=this._lastWindowInfo.Height)
this.Hide();else
this._checkForScrollResizeHandle=window.setTimeout(new Function('window.'+this._variableName+'._checkForScrollResize();'),999);}}
Telligent_PopupPanel.prototype.Hide=function()
{if(this._isShown)
{if(!this._initialized)
this._initialize();this._panel.style.position='absolute';this._panel.style.display='none';this._panelMask.style.position='absolute';this._panelMask.style.display='none';this._isShown=false;this._lastPosition=null;Telligent_Common.ShowSelectBoxes(this._panelMask);if(this._hideOnDocumentClick)
{document.onclick=this._originalDocumentOnClick;this._originalDocumentOnClick=null;}
if(this.OnPanelHideFunction)
this.OnPanelHideFunction();}}
Telligent_PopupPanel.prototype.ClearPanelContent=function()
{while(this._panel.childNodes.length>0)
this._panel.removeChild(this._panel.childNodes[0]);}
Telligent_PopupPanel.prototype.SetPanelContent=function(html)
{this.ClearPanelContent();this._panel.innerHTML=html;this.Refresh();}
Telligent_PopupPanel.prototype.AddNodeToPanel=function(node)
{this._panel.appendChild(node);this.Refresh();}
Telligent_PopupPanel.prototype.RemoveNodeFromPanel=function(node)
{this._panel.removeChild(node);this.Refresh();}
Telligent_PopupPanel.prototype.GetPanelNodes=function()
{return this._panel.childNodes;}
Telligent_PopupPanel.prototype.Refresh=function()
{if(this._animationHandle)
window.clearTimeout(this._animationHandle);if(this._isShown&&this._lastPosition)
this.Show(this._lastPosition.X,this._lastPosition.Y,this._lastPosition.Width,this._lastPosition.Height,true);}
Telligent_PopupPanel.prototype._initialize=function()
{if(document.forms.length==1)
document.forms[0].appendChild(this._panelMask);else
document.body.appendChild(this._panelMask);this._panel.onmouseover=new Function(this._variableName+'._onMouseOver();');this._panel.onmouseout=new Function(this._variableName+'._onMouseOut();');if(!this._panel.innerHTML&&this._initialContentContainer)
{while(this._initialContentContainer.childNodes.length>0)
this.AddNodeToPanel(this._initialContentContainer.childNodes[0]);}
this._initialized=true;}
Telligent_PopupPanel.prototype.Dispose=function()
{if(this._initialized)
{this._panel.onmouseover=null;this._panel.onmouseout=null;if(document&&document.body)
document.body.removeChild(this._panelMask);this._panelMask=null;this._initialized=false;}}
Telligent_PopupPanel.prototype._animate=function(propertyName,targetValue,nextValue,step,acceleration)
{if(this._animationHandle)
window.clearTimeout(this._animationHandle);var currValue=parseInt(eval('this._panel.'+propertyName),10);if((step<0&&currValue<targetValue)||(step>0&&currValue>targetValue)||Math.abs(step)<1)
{eval('this._panel.'+propertyName+' = targetValue + \'px\'');this._panel.style.position='static';this._panelMask.style.overflow='visible';this._animationHandle=null;this._isOpening=false;this._lastWindowInfo=Telligent_Common.GetWindowInfo();this._checkForScrollResizeHandle=window.setTimeout(new Function('window.'+this._variableName+'._checkForScrollResize();'),999);}
else
{eval('this._panel.'+propertyName+' = nextValue + \'px\'');nextValue=nextValue+step;if(step>0&&nextValue>targetValue)
nextValue=targetValue;else if(step<0&&nextValue<targetValue)
nextValue=targetValue;step=step*acceleration;this._animationHandle=window.setTimeout(new Function(this._variableName+'._animate(\''+propertyName+'\','+targetValue+','+nextValue+','+step+','+acceleration+');'),19);}}
Telligent_PopupPanel.prototype._documentOnClick=function()
{if(this._isShown&&!this._isOpening)
this.Hide();if(this._originalDocumentOnClick)
this._originalDocumentOnClick();};