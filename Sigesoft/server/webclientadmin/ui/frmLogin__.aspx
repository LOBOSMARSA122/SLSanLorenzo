<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin__.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.frmLogin1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <link href="CSS/main.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
		
/* Login Style */
.login
{
    position: absolute;
    left: 0px;
    top: 0px;
    width: 1024px;
    height: 680px;
   
    /*<img src="images/Logo/logo_sanjoaquin.png" />*/
}
.login-form
{
    position: absolute;
    left: 704px;
    top: 100px;
    width: 240px;
    height: 520px;
}
.login-title
{
    position: absolute;
    left: 20px;
    top: 40px;
    width: 200px;
    height: 120px;
    font-family: Arial;
    font-size: 36px;
    font-weight: normal;
    color: #333;
    text-align: right;
}
/* ------------------------------------------------------------------------- */

/* Form & Labels Style*/
.form-item
{
    position: absolute;
    display: block;
    width: auto;
}
.form-label
{
    position: absolute;
    font-family: Arial;
    font-size: 8pt;
    color: #000;
    line-height: 20px;
    text-align: left;
}

.form-input
{
    position: absolute;
    font-family: Arial;
    font-size: 8pt;
    text-align: left;
    float: left;
}
.form-textbox
{
    position: absolute;
    padding: 5px;
    font-family: Arial;
    font-size: 8pt;
    line-height: 16px;
    color: #000;
    overflow: auto;
}


/* ------------------------------------------------------------------------- */

* { box-sizing: border-box; padding:0; margin: 0; }

body {
	font-family: "HelveticaNeue-Light","Helvetica Neue Light","Helvetica Neue",Helvetica,Arial,"Lucida Grande",sans-serif;
  color:white;
  font-size:12px;
  background:#A9F5D0 url(/images/classy_fabric.png);
}

form {
 	background:#04B486; 
  width:300px;
  margin:30px auto;
  border-radius:0.4em;
  border:1px solid #191919;
  overflow:hidden;
  position:relative;
  box-shadow: 0 5px 10px 5px rgba(0,0,0,0.2);
}

form:after {
  content:"";
  display:block;
  position:absolute;
  height:1px;
  width:100px;
  left:20%;
  background:linear-gradient(left, #111, #444, #b6b6b8, #444, #111);
  top:0;
}

form:before {
 	content:"";
  display:block;
  position:absolute;
  width:8px;
  height:5px;
  border-radius:50%;
  left:34%;
  top:-7px;
  box-shadow: 0 0 6px 4px #fff;
}

.inset {
 	padding:20px; 
  border-top:1px solid #19191a;
}

form h1 {
  font-size:18px;
  text-shadow:0 1px 0 black;
  text-align:center;
  padding:15px 0;
  border-bottom:1px solid rgba(0,0,0,1);
  position:relative;
}

form h1:after {
 	content:"";
  display:block;
  width:250px;
  height:100px;
  position:absolute;
  top:0;
  left:50px;
  pointer-events:none;
  transform:rotate(70deg);
  background:linear-gradient(50deg, rgba(255,255,255,0.15), rgba(0,0,0,0));
  
}

label {
 	color:#666;
  display:block;
  padding-bottom:9px;
}

input[type=text],
input[type=password] {
 	width:100%;
  padding:8px 5px;
  background:linear-gradient(#E0F8EC,#EFFBFB );
  border:1px solid #222;
  box-shadow:
    0 1px 0 rgba(255,255,255,0.1);
  border-radius:0.3em;
  margin-bottom:20px;
}

label[for=remember]{
 	color:white;
  display:inline-block;
  padding-bottom:0;
  padding-top:5px;
}

input[type=checkbox] {
 	display:inline-block;
  vertical-align:top;
}

.p-container {
 	padding:0 20px 20px 20px; 
}

.p-container:after {
 	clear:both;
  display:table;
  content:"";
}

.p-container span {
  display:block;
  float:left;
  color:#0d93ff;
  padding-top:8px;
}

input[type=submit] {
 	padding:5px 20px;
  border:1px solid rgba(0,0,0,0.4);
  text-shadow:0 -1px 0 rgba(0,0,0,0.4);
  box-shadow:
    inset 0 1px 0 rgba(255,255,255,0.3),
    inset 0 10px 10px rgba(255,255,255,0.1);
  border-radius:0.3em;
  background:#0184ff;
  color:white;
  float:right;
  font-weight:bold;
  cursor:pointer;
  font-size:13px;
}

input[type=submit]:hover {
  box-shadow:
    inset 0 1px 0 rgba(255,255,255,0.3),
    inset 0 -10px 10px rgba(255,255,255,0.1);
}

input[type=text]:hover,
input[type=password]:hover,
label:hover ~ input[type=text],
label:hover ~ input[type=password] {
 	background:#F2F5A9;
}
		</style>
    <script type="text/javascript">
        function runScript(e) {
     
            if (e.keyCode == 13) {          
                var tb = document.getElementById("got");
                tb.click();
            }
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
<img src="images/Logo/logo_sanjoaquin.png" width="300" height="370" />
     
  <div class="inset">
  <p>
    <label for="email">USUARIO</label>
    <input type="text" runat="server" name="email" id="email"/>
  </p>
  <p>
    <label for="password">CONTRASEÑA</label>
    <input type="password" name="password" id="password" runat="server"  onkeypress="return runScript(event) "/>
  </p>
  </div>
  <p class="p-container">
    <input type="button" name="go" id="got" value="Acceder" runat="server" onserverclick="go_Click"/>
  </p>
         <p>
    <input type="text" runat="server" name="Mensaje" id="Text1" readonly="true" style="font-family: Arial, Helvetica, sans-serif; font-size: xx-small; color: #FF0000" />
    </p>

    </form>
</body>
</html>
