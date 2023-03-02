using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor_racks.Models
{
    public static class clsAsignar
    {
        public static void Asignacion(clsRackModel oRack, List<Label> oRackString, List<Image> oImgRack, bool fRack, double width = 0, double height = 0)
        {
            Puertas(oRack, oRackString, oImgRack);
            Energia(oRack, oRackString, oImgRack);
            Luz(oRack, oRackString, oImgRack);
            Humedad(oRack, oRackString, oImgRack);
            Temperatura(oRack, oRackString, oImgRack);

            for (int i = 0; i < 5; i++)
            {
                oImgRack[i].HeightRequest = fRack
                    ? DeviceInfo.Current.Idiom == DeviceIdiom.Tablet ? 140 : 75
                    : DeviceInfo.Current.Idiom == DeviceIdiom.Tablet ? 90 : width > height ? 30 : 50;

                oRackString[i].FontSize = DeviceInfo.Current.Idiom == DeviceIdiom.Tablet ? 25 : width > height ? 12 : 15;
                oRackString[i].VerticalOptions = LayoutOptions.Center;
                oRackString[i].HorizontalOptions = LayoutOptions.Center;
            }
        }

        private static void Puertas(clsRackModel oRack, List<Label> oRackString, List<Image> oImgRack)
        {
            oImgRack[0].Source = !oRack.Puertas ? "puertacerr.png" : "puertaopen.png";
            oRackString[0].Text = !oRack.Puertas ? "Cerrada" : "Abierta";
        }

        private static void Energia(clsRackModel oRack, List<Label> oRackString, List<Image> oImgRack)
        {
            oImgRack[1].Source = !oRack.Energia ? "energia.png" : "rayocol.png";
            oRackString[1].Text = !oRack.Energia ? "No hay energia" : "Hay energia";
        }

        private static void Luz(clsRackModel oRack, List<Label> oRackString, List<Image> oImgRack)
        {
            oImgRack[2].Source = !oRack.Luz ? "focoapg.png" : "focopren.png";
            oRackString[2].Text = !oRack.Luz ? "Apagada" : "Encendida";
        }

        private static void Humedad(clsRackModel oRack, List<Label> oRackString, List<Image> oImgRack)
        {
            oRackString[3].Text = $"{oRack.Humedad} %";
            if (oRack.Humedad <= 10) oImgRack[3].Source = "gotavacia.png";
            else if (oRack.Humedad > 10 && oRack.Humedad <= 20) oImgRack[3].Source = "gota10.png";
            else if (oRack.Humedad > 20 && oRack.Humedad <= 30) oImgRack[3].Source = "gota20.png";
            else if (oRack.Humedad > 30 && oRack.Humedad <= 40) oImgRack[3].Source = "gota30.png";
            else if (oRack.Humedad > 40 && oRack.Humedad <= 50) oImgRack[3].Source = "gota40.png";
            else if (oRack.Humedad > 50 && oRack.Humedad <= 60) oImgRack[3].Source = "gota50.png";
            else if (oRack.Humedad > 60 && oRack.Humedad <= 70) oImgRack[3].Source = "gota60.png";
            else if (oRack.Humedad > 70 && oRack.Humedad <= 80) oImgRack[3].Source = "gota70.png";
            else if (oRack.Humedad > 80 && oRack.Humedad <= 90) oImgRack[3].Source = "gota80.png";
            else if (oRack.Humedad >= 90) oImgRack[3].Source = "gota90.png";
        }

        private static void Temperatura(clsRackModel oRack, List<Label> oRackString, List<Image> oImgRack)
        {
            oRackString[4].Text = $"{oRack.Temperatura} °C";
            if (oRack.Temperatura <= 0) oImgRack[4].Source = "termovacio.png";
            else if (oRack.Temperatura > 0 && oRack.Temperatura <= 5) oImgRack[4].Source = "termo1.png";
            else if (oRack.Temperatura > 5 && oRack.Temperatura <= 10) oImgRack[4].Source = "termo2.png";
            else if (oRack.Temperatura > 10 && oRack.Temperatura <= 15) oImgRack[4].Source = "termo3.png";
            else if (oRack.Temperatura > 15 && oRack.Temperatura <= 20) oImgRack[4].Source = "termo4.png";
            else if (oRack.Temperatura > 20 && oRack.Temperatura <= 25) oImgRack[4].Source = "termo5.png";
            else if (oRack.Temperatura > 25) oImgRack[4].Source = "termofull.png";
        }
    }
}
