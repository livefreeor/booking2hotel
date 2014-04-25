function popup (url,inthight,intwidth)
{
var windowprops = "height="+inthight+",width="+intwidth+",top="+ (screen.height - inthight)/2+ ",left=" + (screen.width - intwidth)/2 +",location=yes," + "scrollbars=yes,menubars=no,toolbars=no,resizable=yes";
popuphelp = window.open(url,"MenuPopup",windowprops);
popuphelp.focus();
}

function popup_nobar(url,inthight,intwidth)
{
var windowprops = "height="+inthight+",width="+intwidth+",top="+ (screen.height - inthight)/2+ ",left=" + (screen.width - intwidth)/2 +",location=no," + "scrollbars=no,menubars=no,toolbars=no,resizable=no";
popuphelp = window.open(url,"MenuPopup",windowprops);
popuphelp.focus();
}

function close_reload()
{
    opener.location.reload(true);
    self.close();
}

function goback() {
    history.go(-1);
}

function winNew(strlink,intHeight,intWidth){

      stringa="width="+intWidth+",height="+intHeight;
      window.open(strlink,"",stringa);
}