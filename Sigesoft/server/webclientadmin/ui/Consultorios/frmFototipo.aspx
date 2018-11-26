<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmFototipo.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmFototipo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
		
body{
				margin:0px;
				padding:0px;
			}
			#imgCanvas{
				border: 1px solid  #000;
				box-shadow: 2px 2px 10px #333;
			}
			#botonera{
				margin-left: 20px;
				margin-top: 20px;
			}
			.botones{
				display: inline;
				height: 32px;
				width: 32px;
			}
			#rellenoFondo{
				background: #333;
				border: 2px solid #000;
				border-radius: 5px 5px 5px 5px;
				height: 50px;
				margin-bottom: 10px;
				width: 50px;				
			}
	</style>
</head>
    
<body>

    <form id="form1" runat="server">
    <div id="imagen" ></div>
    <canvas id="imgCanvas" width="800" height="800"></canvas>
    <div id="botonera">
		
		<div id="rellenoFondo" ></div>

		<div class="botones" onclick="cambioColor('333')"><img src="negro.png"/></div>
		<div class="botones" onclick="cambioColor('fff')"><img src="blanco.png"/></div>
		<div class="botones" onclick="cambioColor('fe0000')"><img src="rojo.png"/></div>
		<div class="botones" onclick="cambioColor('800080')"><img src="morado.png"/></div>
		<div class="botones" onclick="cambioColor('ffff00')"><img src="amarillo.png"/></div>
		<div class="botones" onclick="cambioColor('00ff00')"><img src="verde.png"/></div>
		<div class="botones" onclick="cambioColor('00ffff')"><img src="azulClaro.png"/></div>
		<div class="botones" onclick="cambioColor('0000fe')"><img src="azulOscuro.png"/></div>
		<div class="botones" id="fondo" onclick="cambioFondo()"><img src="bg.png"/></div>
		<div  class="botones" onclick="cargarDibujo()"><img src="reset.png" style="width:32px"/></div>
		<div  class="botones" onclick="Guardar()"><img src="save.png" style="width:35px"/></div>
	</div>
    </form>
</body>

    <script >
    var canvas = document.getElementById("imgCanvas");
    var context = canvas.getContext("2d");

    var imagenFondo = new Image();

    imagenFondo.src = 'rostro.png';
    //Cargo la imagen en la posición
    context.drawImage(imagenFondo, 0, 0);


    imagenFondo.onload = function () {
        context.drawImage(imagenFondo, 0, 0);
    }

    var estoyDibujando = false;

    function cargarDibujo() {

        context.clearRect(0, 0, canvas.width, canvas.height);
        var imagenFondo = new Image();
        imagenFondo.src = 'rostro.png';
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
            context.fillRect(posx, posy, 10, 10);
        }
    }

    function levantaRaton(e) {
        //Indico que termino el dibujo
        context.closePath();
        estoyDibujando = false;
    }
    ////////////////////////////////////////////////////

    //---------------------------------//
    //document.addEventListener('mousemove', draw, false);

    document.addEventListener('mousemove', mueveRaton, false);

    document.addEventListener('mousedown', pulsaRaton, false);

    document.addEventListener('mouseup', levantaRaton, false);
    color = "#333";
    //---------------------------------//

    function getMousePos(canvas, evt) {
        var rect = canvas.getBoundingClientRect();
        return {
            x: (evt.clientX - rect.left) / (rect.right - rect.left) * canvas.width,
            y: (evt.clientY - rect.top) / (rect.bottom - rect.top) * canvas.height
        };
    }
    </script>
</html>
