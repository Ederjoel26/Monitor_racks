using System.Net.NetworkInformation;

namespace Monitor_racks.Views;

public partial class Servicios : ContentPage
{
    private string[] sIPAddress =
    {
        "https://www.google.com/",
        "127.012.120.222",
        "127.011.20.1",
        "192.168.0.12",
        "https://asd.com"
    };
    private double fWidth = 0;
    private double fHeight = 0;
    private Label[] lblServicios;
    private Label[] lblEstatusServicios;
    private int Id = 0;

    public Servicios()
    {
        InitializeComponent();

        fWidth = Width;
        fHeight = Height;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Title = "Servicios";

        lblServicios = fWidth > fHeight
            ? new Label[] { Servicio1H, Servicio2H, Servicio3H, Servicio4H, Servicio5H }
            : new Label[] { Servicio1V, Servicio2V, Servicio3V, Servicio4V, Servicio5V };


        lblEstatusServicios = fWidth > fHeight
            ? new Label[] { EstatusServicio1H, EstatusServicio2H, EstatusServicio3H, EstatusServicio4H, EstatusServicio5H }
            : new Label[] { EstatusServicio1V, EstatusServicio2V, EstatusServicio3V, EstatusServicio4V, EstatusServicio5V};

        int Estatus = 0;

        foreach (var s in lblServicios)
        {
            s.Text = "Cargando";
            s.HorizontalOptions = LayoutOptions.Center;
            s.VerticalOptions = LayoutOptions.Center;
            lblEstatusServicios[Estatus].Text = "Cargando...";
            Estatus++;
        }

        Dispatcher.StartTimer(TimeSpan.FromSeconds(5), () =>
        {
            foreach (string Address in sIPAddress)
            {
                verificarServicio(Id, Address, lblServicios[Id], lblEstatusServicios[Id]);
                Id++;
            }
            Id = 0;
            return true;
        });

    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (fWidth != width || fHeight != height)
        {
            fWidth = width;
            fHeight = height;

            ActualizarVista();

            lblServicios = fWidth > fHeight
                ? new Label[] { Servicio1H, Servicio2H, Servicio3H, Servicio4H, Servicio5H }
                : new Label[] { Servicio1V, Servicio2V, Servicio3V, Servicio4V, Servicio5V };


            lblEstatusServicios = fWidth > fHeight
                ? new Label[] { EstatusServicio1H, EstatusServicio2H, EstatusServicio3H, EstatusServicio4H, EstatusServicio5H }
                : new Label[] { EstatusServicio1V, EstatusServicio2V, EstatusServicio3V, EstatusServicio4V, EstatusServicio5V };
            
            for(int i = 0; i < 5; i++)
            {
                lblEstatusServicios[i].Text = "Cargando...";
                lblServicios[i].Text = "Cargando...";
            }
        }
    }

    private async void ActualizarVista()
    {
        if (fWidth > fHeight)
        {
            cvVerticalServicios.IsVisible = false;
            cvHorizontalServicios.IsVisible = true;
        }
        else
        {
            cvVerticalServicios.IsVisible = true;
            cvHorizontalServicios.IsVisible = false;
        }
    }

    private void verificarServicio(int Id, string IPAddress, Label lblServicio, Label lblEstatus)
    {
        lblServicio.Text = IPAddress;
        try
        {
            Ping ping = new Ping();
            PingReply Replicar = ping.Send(IPAddress, 1000);
            if (Replicar != null)
            {
                lblEstatus.Text = $"Estatus: {Replicar.Status} \n " +
                                    $"Time: {Replicar.RoundtripTime} \n " +
                                    $"Address: {Replicar.Address}";

                lblEstatus.BackgroundColor = Color.Parse("green");
            }
        }
        catch
        {
            lblEstatus.Text = "Error: se agotó el tiempo de respuesta";
            lblEstatus.BackgroundColor = Color.Parse("red");
        }
    }
}