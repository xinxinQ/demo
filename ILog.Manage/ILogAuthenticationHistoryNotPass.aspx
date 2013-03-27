<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ILogAuthenticationHistoryNotPass.aspx.cs"
    Inherits="ILog.Manage.ILogAuthenticationHistoryNotPass" %>

<form id="formsExit" name="formsExit" action="ILogAuthenticationHistoryNotPass.aspx"
method="post" onsubmit="return checkFormsExit(this);return false;" style="margin: 0px;
padding: 0px;">
<table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
    <tr class="hback">
        <td>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1">
                <tr>
                    <td>                        
                        <input id="number" name="number" type="hidden"  value="<%=userId %>" />
                        <input id="aid" name="aid" type="hidden"  value="<%=aid %>" />
                        <input id="type" name="type" type="hidden"  value="<%=type %>" />
                        <input type="radio" value="0" id="returnedOne" name="reason" checked="checked" onclick="checkRadioReturn(this);"/>
                        基本信息不完整；
                        <br />
                        <br />
                        <input type="radio" value="1" id="returnedTwo" name="reason"onclick="checkRadioReturn(this);"/>
                        证件扫描不清晰；<br /><br />
                        <input type="radio" value="2" id="returnedShree" name="reason" onclick="checkRadioReturn(this);"/>
                        真实姓名与身份证不符；<br /><br />
                        <input type="radio" value="3" id="returnedFour" name="reason" onclick="checkRadioReturn(this);"/>
                        职业证明无效；<br /><br />
                        <input type="radio" value="4" id="returnedFive" name="reason" onclick="checkRadioReturn(this);"/>
                        认证说明填写不清晰；<br /><br />
                        <input type="radio" value="5" id="returnOther" name="reason" onclick="checkRadioReturn(this);" />
                        其它<br /><br />
                    </td>
                </tr>
                <tr id="exitContentTr" style="display: none;">
                    <td valign="top">
                        <textarea name="exitReason" cols="50" rows="5" id="exitReason"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        <input type="submit" style="cursor: pointer;" name="Submit" value="确定" /><span style="padding-left: 12px;
                            color: Red;" id="waitID"></span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</form>
