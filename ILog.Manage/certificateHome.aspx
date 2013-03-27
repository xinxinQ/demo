<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="certificateHome.aspx.cs"
    Inherits="ILog.Manage._certificateHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="http://www.instrument.com.cn/admin/images/style.css" rel="stylesheet"
        type="text/css" />
    <title>Ilog认证后台管理</title>
    <script language="JavaScript" type="text/JavaScript">
    
    //CSS背景控制
function overColor(Obj)
{
	var elements=Obj.childNodes;
	for(var i=0;i<elements.length;i++)
	{
		elements[i].className="hback_1"
		Obj.bgColor="";//颜色要改
	}
	
}
function outColor(Obj)
{
	var elements=Obj.childNodes;
	for(var i=0;i<elements.length;i++)
	{
		elements[i].className="hback";
		Obj.bgColor="";
	}
}    
    </script>
</head>
<body leftmargin="3" topmargin="3" marginwidth="3" marginheight="3">
    <form name="forms" method="post" action="">
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
        <tr class="hback">
            <td class="xingmu">
                <a href="certificateHome.aspx?type=1">个人认证</a>&nbsp;┆&nbsp; <a href="certificateHome.aspx?type=2">
                    名人认证</a>
            </td>
        </tr>
        <tr>
            <td class="hback">
            
            <%=navigationHtml %>
            
                <a href="certificateHome.aspx?type=<%=type %>&state=0" title="待审批"
                    class="menu">待审批</a>&nbsp;┆&nbsp;<a href="certificateHome.aspx?type=<%=type %>&state=1"
                        title="已审批" class="menu">已审批</a>&nbsp;┆&nbsp;<a href="certificateHome.aspx?type=<%=type %>&state=2"
                            title="已驳回" class="menu">已驳回</a>
                            
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
        <tr class="hback">
            <td>
                <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1">
                    <tr>
                        <td>
                            您的位置：
                            <%=currentLocationHtml%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <%=certificateHtml%>
    </table>
    <%=pageHtml%>
    
    </form>
</body>
</html>
