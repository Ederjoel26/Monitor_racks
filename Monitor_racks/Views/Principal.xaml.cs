
namespace Monitor_racks.Views;

public partial class Principal : ContentPage
{
    private double fWidth = 0;
    private double fHeight = 0;
    public Principal()
    {
        InitializeComponent();

        fWidth = Width;
        fHeight = Height;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType != NetworkAccess.Internet)
        {
            await DisplayAlert("No tiene acceso a internet.", "Favor de rectificar su conexión.", "Ok");
            Thread.Sleep(1000);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
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
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Site?ID=1");
    }

    private async void ImageButton_Clicked_1(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Site?ID=2");
    }

    private async void ImageButton_Clicked_2(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Site?ID=3");
    }

    private void ActualizarVista()
    {
        gPrincipal?.RowDefinitions.Clear();
        gPrincipal?.ColumnDefinitions.Clear();
        gPrincipal?.Children.Clear();

        if (fWidth > fHeight)
        {
            gPrincipal?.Add(ActualizarALandscapteLayout(), 0, 1);
        }
        else
        {
            gPrincipal?.Add(ActualizarAPortraitLayout(), 0, 1);
        }
    }

    private Grid ActualizarALandscapteLayout()
    {
        InicializarTitulo();

        Grid gSite = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(33, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(33, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(33, GridUnitType.Star) }
            }
        };

        Grid gSite1 = CrearSite("Site 1", 1);
        Grid gSite2 = CrearSite("Site 2", 2);
        Grid gSite3 = CrearSite("Site 3", 3);

        gSite.Add(gSite1, 0, 0);
        gSite.Add(gSite2, 1, 0);
        gSite.Add(gSite3, 2, 0);

        return gSite;
    }

    private Grid ActualizarAPortraitLayout()
    {
        InicializarTitulo();

        Grid gSite = new Grid()
        {
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(33, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(33, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(33, GridUnitType.Star) },
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(80, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) },
            }
        };

        Grid gSite1 = CrearSite("Site 1", 1);
        Grid gSite2 = CrearSite("Site 2", 2);
        Grid gSite3 = CrearSite("Site 3", 3);

        gSite.Add(gSite1, 1, 0);
        gSite.Add(gSite2, 1, 1);
        gSite.Add(gSite3, 1, 2);

        return gSite;
    }

    private Grid CrearSite(string Titulo, int numeroSite)
    {
        Grid gSite1 = new Grid()
        {
            Margin = 5,
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(85, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(15, GridUnitType.Star) },
            }
        };

        ImageButton imgbtnSite1 = new ImageButton
        {
            Source = "serv.jpg",
            CornerRadius = 20,
            BackgroundColor = Color.Parse("white"),
            BorderColor = Color.Parse("black"),
            BorderWidth = 1,
        };

        if (numeroSite == 1)
            imgbtnSite1.Clicked += (sender, args) => ImageButton_Clicked(sender, args);
        else if (numeroSite == 2)
            imgbtnSite1.Clicked += (sender, args) => ImageButton_Clicked_1(sender, args);
        else
            imgbtnSite1.Clicked += (sender, args) => ImageButton_Clicked_2(sender, args);

        Label lblTituloSite1 = new Label
        {
            Text = Titulo,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold,
            FontSize = DeviceInfo.Current.Idiom == DeviceIdiom.Tablet ? 25 : 15,
            FontAutoScalingEnabled = false
        };

        gSite1.Add(imgbtnSite1, 0, 0);
        gSite1.Add(lblTituloSite1, 0, 1);

        return gSite1;
    }

    private void InicializarTitulo()
    {
        gPrincipal.AddRowDefinition(new RowDefinition { Height = new GridLength(10, GridUnitType.Star) });
        gPrincipal.AddRowDefinition(new RowDefinition { Height = new GridLength(90, GridUnitType.Star) });

        Label lblTitulo = new Label
        {
            FontAttributes = FontAttributes.Bold,
            Text = "Selecciona un site",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = DeviceInfo.Current.Idiom == DeviceIdiom.Tablet ? 50 : 25,
            FontAutoScalingEnabled = false
        };

        gPrincipal.Add(lblTitulo, 0, 0);
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("Servicios");
    }

}