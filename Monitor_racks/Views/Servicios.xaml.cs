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

    private Label Servicio1 = new Label(), Servicio2 = new Label(), Servicio3 = new Label(), Servicio4 = new Label(), Servicio5 = new Label();
    private Label EstatusServicio1 = new Label(), EstatusServicio2 = new Label(), EstatusServicio3 = new Label(), EstatusServicio4 = new Label(), EstatusServicio5 = new Label();

    private int Id = 0;

    public Servicios()
    {
        InitializeComponent();

        fWidth = Width;
        fHeight = Height;

        Label[] lblServicios =
        {
            Servicio1,
            Servicio2,
            Servicio3,
            Servicio4,
            Servicio5
        };
        Label[] lblEstatusServicios =
        {
            EstatusServicio1,
            EstatusServicio2,
            EstatusServicio3,
            EstatusServicio4,
            EstatusServicio5
        };

        int Estatus = 0;
        foreach (var s in lblServicios)
        {
            s.Text = "Cargando";
            s.HorizontalOptions = LayoutOptions.Center;
            s.VerticalOptions = LayoutOptions.Center;
            lblEstatusServicios[Estatus].Text = "Cargando";
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
        }
    }

    private async void ActualizarVista()
    {
        gServicios?.RowDefinitions.Clear();
        gServicios?.ColumnDefinitions.Clear();
        gServicios?.Children.Clear();

        if (fWidth > fHeight)
        {
            await Shell.Current.GoToAsync("Principal", false);
            await Shell.Current.GoToAsync("ServiciosHorizontal");
        }
        else
        {
            gServicios?.Add(ActualizarPortraitLayout(), 0, 1);
        }
    }

    private Frame ActualizarPortraitLayout()
    {
        InicializarTitulo();

        Frame fServiciosLayout = new Frame
        {
            BackgroundColor = Color.Parse("white"),
            CornerRadius = 5,
            Padding = 0,
            Margin = 5,
            BorderColor = Color.Parse("black")
        };

        Grid gServiciosLayout = new Grid()
        {
            RowDefinitions =
            {
                new RowDefinition{ Height = new GridLength(20, GridUnitType.Star) },
                new RowDefinition{ Height = new GridLength(20, GridUnitType.Star) },
                new RowDefinition{ Height = new GridLength(20, GridUnitType.Star) },
                new RowDefinition{ Height = new GridLength(20, GridUnitType.Star) },
                new RowDefinition{ Height = new GridLength(20, GridUnitType.Star) },
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star) }
            },
            RowSpacing = 2,
            ColumnSpacing = 2
        };

        gServiciosLayout.Add(Servicio1, 0, 0);
        gServiciosLayout.Add(EstatusServicio1, 1, 0);
        gServiciosLayout.Add(Servicio2, 0, 1);
        gServiciosLayout.Add(EstatusServicio2, 1, 1);
        gServiciosLayout.Add(Servicio3, 0, 2);
        gServiciosLayout.Add(EstatusServicio3, 1, 2);
        gServiciosLayout.Add(Servicio4, 0, 3);
        gServiciosLayout.Add(EstatusServicio4, 1, 3);
        gServiciosLayout.Add(Servicio5, 0, 4);
        gServiciosLayout.Add(EstatusServicio5, 1, 4);

        fServiciosLayout.Content = gServiciosLayout;

        return fServiciosLayout;
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
            lblEstatus.Text = "Error: se agot� el tiempo de respuesta";
            lblEstatus.BackgroundColor = Color.Parse("red");
        }
    }

    private void InicializarTitulo()
    {
        gServicios.AddRowDefinition(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });
        gServicios.AddRowDefinition(new RowDefinition { Height = new GridLength(95, GridUnitType.Star) });

        Label lblTitulo = new Label()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = DeviceInfo.Current.Idiom == DeviceIdiom.Tablet ? 50 : 25,
            Text = "Servicios",
            FontAttributes = FontAttributes.Bold
        };

        gServicios.Add(lblTitulo, 0, 0);
    }
}