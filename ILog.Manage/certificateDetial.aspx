<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="certificateDetial.aspx.cs"
    Inherits="ILog.Manage.certificateDetial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="http://www.instrument.com.cn/admin/images/style.css" rel="stylesheet"
        type="text/css" />
    <title>��֤������Ϣ</title>

    <script src="js/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="js/dialog.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">    
     
     
       //ѡ��������ʾ�������ɿ�
        function checkRadioReturn(obj)
        {       
                     
            if ($(obj).val()=="5")
			{			    
			    
				$("#exitContentTr").css({"display":"block"});
				
			}
			else
			{
			    
				$("#exitContentTr").css({"display":"none"});
				
			}
        
        }

        //�˻�У��
	    function checkFormsExit(theform)
        {
        
            if($("#returnOther").attr("checked"))
            {  
                   
	            if ($("#exitReason").val().replace(/(^\s*)|(\s*$)/g, "")=="")
	            {
		            alert("����д�˻�����!");
		            $("#exitReason").focus();
		            return false;
            		
	            }
	        	
	        }	        
	        
	        ajaxpost_bbs(theform);
        	
        }  
        
        //�ܾ�
        function Returned()
        {              
	      
	          $("#btnRefuse").attr("disabled","disabled");          
	   	      var userId=$("#number").val(); 
	   	      var aId=$("#aid").val(); 
	   	       var type=$("#type").val();	   	          	
	          opendialog("ILogAuthenticationHistoryNotPass.aspx?userid="+userId+"&type="+type+"&aid="+aId+"&state=2",'�ܾ�����',500,450);
	          return false;
	          
        }    
    
        //���ͨ��
        function checkForm(theforms)
        {  
        
	        if (confirm("ȷ��Ҫ����ͨ������֤������"))
	        {                   
	           
	   	       return true;           
	    			
	        }else
	        {
	        
	            return false;
	            
	        }  
	        
	       ajaxpost_bbs(theforms);
	       
        }    
        
         function document.onkeydown() {
           
            if (
            (event.keyCode==8)  ||                 //�����˸�ɾ���� 
                (event.keyCode == 116) ||                 //���� F5 ˢ�¼� 
                (event.ctrlKey && event.keyCode == 82))  //Ctrl + R 
            {
                event.keyCode = 0;
                event.returnValue = false;
            }
           
        }
    
    
    </script>

</head>
<body leftmargin="3" topmargin="3" marginwidth="3" marginheight="3">

    <form id="forms" action="certificateDetial.aspx" method="post" onsubmit="return checkForm(this);return false;">
    
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
        <tr class="hback">
            <td class="xingmu">
                <a href="certificateHome.aspx?type=1">������֤</a>&nbsp;��&nbsp; <a href="certificateHome.aspx?type=2">
                    ������֤</a>
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
        <tr class="hback">
            <td>
                <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1">
                    <tr>
                        <td>
                            ����λ�ã�
                            <%=currentLocationHtml%>
                        </td>
                        <td align="right">
                            <input name="btn_back" type="button" id="Button1" style="cursor: pointer;" value="������һҳ"
                                onclick="javascript:history.back();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" class="table">
        <tr>
            <td>
            
            <%=certificateDetialHtml %>
                
                
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
