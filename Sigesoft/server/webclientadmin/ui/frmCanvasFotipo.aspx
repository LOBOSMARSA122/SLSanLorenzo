<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCanvasFotipo.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.frmCanvasFotipo" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
	<style type="text/css">
		
		#imgCanvas{
			border: 1px solid  #000;
			box-shadow: 2px 2px 10px #333;
		}
		#botonera{
			margin-left: 20px;
			margin-top: 20px;
		}
		.botones{
			display: inline-block;
			height: 32px;
			width: 32px;
            transition: all 0.3s ease;
		}

        #btn1 {
            background-color: #fffc6d
        }
        #btn1:hover {
            transform:translateY(-8px)
        }

        #btn2 {
            background-color: #4286f4
        }#btn2:hover {
             transform:translateY(-8px)
        }

        #btn3 {
            background-color:#f44171
        }#btn3:hover {
            transform:translateY(-8px)
        }
        #btn4 {
            background-color: #25dd69
        }#btn4:hover {
            transform:translateY(-8px)
        }

		#rellenoFondo{
			background: #333;
			border: 2px solid #000;
			border-radius: 5px 5px 5px 5px;
			height: 50px;
			margin-bottom: 10px;
			width: 50px;
				
		}

        #cargarDibujo:hover {
            transform:rotate(-360deg);
            transition: all 2s linear;
            cursor:pointer;
        }
	</style>
</head>
<body>
    <form runat="server">
        <x:TextBox ID="txtLinkImage" style="display:none" Width="100" runat="server"></x:TextBox>
        <x:TextBox ID="txtMultimediaId" style="display:none" Width="100" runat="server"></x:TextBox>
        <x:TextBox ID="txtSetLinkImg" style="display:none" Width="100" runat="server"></x:TextBox>
    </form>
    
<div>
    <div id="rellenoFondo" ></div>
    <div class="botones" id="btn1" onclick="cambioColor('fffc6d')"></div>
    <div class="botones" id="btn2" onclick="cambioColor('4286f4')"></div>
    <div class="botones" id="btn3" onclick="cambioColor('f44171')"></div>
    <div class="botones" id="btn4" onclick="cambioColor('25dd69')"></div>
    <div class="botones" id="fondo" onclick="cambioFondo()"><img  src="../images/Fototipo/bg.png"/></div>
    <div  class="botones" id="Div1" onclick="Visualizar()"><img style="width:35px; height:35px; cursor:pointer"  src="../images/Fototipo/eye.jpg" /></div>
    <div  class="botones" id="cargarDibujo" onclick="cargarDibujo()"><img src="../images/Fototipo/reset.png" style="width:32px"/></div>
    <div  class="botones" onclick="Guardar()"><img src="../images/Fototipo/save.png" style="width:35px; cursor:pointer"/></div>
    <a id="download" style="display:none"></a>
</div>
<canvas id="imgCanvas" width="800" height="800"></canvas>
                                                                    


<script>


    var canvas = document.getElementById("imgCanvas");
    var context = canvas.getContext("2d");

    var imagenFondo = new Image();

    imagenFondo.src = '../images/Fototipo/rostro.png';



    //Cargo la imagen en la posición
    context.drawImage(imagenFondo, 0, 0);


    imagenFondo.onload = function () {
        context.drawImage(imagenFondo, 0, 0);
    }

    var estoyDibujando = false;

    function cargarDibujo() {

        context.clearRect(0, 0, canvas.width, canvas.height);
        var imagenFondo = new Image();

        imagenFondo.src = '../images/Fototipo/rostro.png';
        //Cargo la imagen en la posición
        context.drawImage(imagenFondo, 0, 0);
    }



    function cambioColor(reciboColor) {
        //Guardo en color el color seleccionado
        color = "#" + reciboColor;
        //Cambio de color el botón de relleno de fondo
        document.getElementById('rellenoFondo').style.background = color;
    }


    function cambioGrosor(reciboGrosor) {
        //Guardo el grosor recibido
        grosor = reciboGrosor;


    }

    function cambioFondo() {
        document.getElementById('imgCanvas').style.background = color;
    }

    function pulsaRaton(e) {
        estoyDibujando = true;
        //Indico que vamos a dibujar
        context.beginPath();

        //Averiguo las coordenadas X e Y por dónde va pasando el ratón
        var pos = getMousePos(canvas, e);
        posx = pos.x;
        posy = pos.y;
        context.moveTo(e.posx, e.posx);
    }

    function mueveRaton(e) {
        if (estoyDibujando) {

            var pos = getMousePos(canvas, e);
            posx = pos.x;
            posy = pos.y;

            context.fillStyle = color;
            context.fillRect(posx, posy, 3, 3);
        }
    }

    function levantaRaton(e) {
        //Indico que termino el dibujo
        context.closePath();
        estoyDibujando = false;
    }

    document.addEventListener('mousemove', mueveRaton, false);

    document.addEventListener('mousedown', pulsaRaton, false);

    document.addEventListener('mouseup', levantaRaton, false);
    color = "#333";

    function getMousePos(canvas, evt) {
        var rect = canvas.getBoundingClientRect();
        return {
            x: (evt.clientX - rect.left) / (rect.right - rect.left) * canvas.width,
            y: (evt.clientY - rect.top) / (rect.bottom - rect.top) * canvas.height
        };
    }


    function Guardar() {
        var link = document.getElementById("download");
        link.href = canvas.toDataURL("image/png");
        asignarInput(link.href);
    }

    function asignarInput(link) {
        var contenedorImg = document.getElementById("<%=txtLinkImage.ClientID%>");
            contenedorImg.value = link;
        }

        function Visualizar() {
            var imagenFondo = new Image();
            var contenedorImg = document.getElementById("<%=txtSetLinkImg.ClientID%>").value;
            imagenFondo.src = contenedorImg;
            context.drawImage(imagenFondo, 0, 0);
        }



</script>

</body>

</html>
