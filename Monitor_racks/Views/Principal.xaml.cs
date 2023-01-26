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

        ActualizarVista();
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
        gPrincipal.RowDefinitions.Clear();
        gPrincipal.ColumnDefinitions.Clear();
        gPrincipal.Children.Clear();

        if (fWidth > fHeight)
        {
            ActualizarALandscapteLayout();
        }
        else
        {
            ActualizarAPortraitLayout();
        }
    }

    private void ActualizarALandscapteLayout()
    {
        InicializarTitulo();

        Grid gSite = new Grid();

        gSite.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(33, GridUnitType.Star) });
        gSite.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(33, GridUnitType.Star) });
        gSite.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(33, GridUnitType.Star) });

        Grid gSite1 = CrearSite("Site1", 1);
        Grid gSite2 = CrearSite("Site2", 2);
        Grid gSite3 = CrearSite("Site3", 3);

        gSite.Children.Add(gSite1);
        gSite.Children.Add(gSite2);
        gSite.Children.Add(gSite3);

        gSite.SetColumn(gSite1, 0);
        gSite.SetColumn(gSite2, 1);
        gSite.SetColumn(gSite3, 2);

        gPrincipal.Children.Add(gSite);
        gPrincipal.SetRow(gSite, 1);
    }

    private void ActualizarAPortraitLayout()
    {
        InicializarTitulo();

        Grid gSite = new Grid();

        gSite.AddRowDefinition(new RowDefinition { Height = new GridLength(33, GridUnitType.Star) });
        gSite.AddRowDefinition(new RowDefinition { Height = new GridLength(33, GridUnitType.Star) });
        gSite.AddRowDefinition(new RowDefinition { Height = new GridLength(33, GridUnitType.Star) });
        gSite.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
        gSite.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(80, GridUnitType.Star) });
        gSite.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });

        Grid gSite1 = CrearSite("Site 1", 1);
        Grid gSite2 = CrearSite("Site 2", 2);
        Grid gSite3 = CrearSite("Site 3", 3);

        gSite.Children.Add(gSite1);
        gSite.Children.Add(gSite2);
        gSite.Children.Add(gSite3);

        gSite.SetRow(gSite1, 0);
        gSite.SetColumn(gSite1, 1);
        gSite.SetRow(gSite2, 1);
        gSite.SetColumn(gSite2, 1);
        gSite.SetRow(gSite3, 2);
        gSite.SetColumn(gSite3, 1);

        gPrincipal.Children.Add(gSite);
        gPrincipal.SetRow(gSite, 1);
    }

    private Grid CrearSite(string Titulo, int numeroSite)
    {
        Grid gSite1 = new Grid()
        {
            Margin = 5
        };

        gSite1.AddRowDefinition(new RowDefinition { Height = new GridLength(85, GridUnitType.Star) });
        gSite1.AddRowDefinition(new RowDefinition { Height = new GridLength(15, GridUnitType.Star) });

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
            FontSize = 15
        };

        gSite1.Children.Add(imgbtnSite1);
        gSite1.Children.Add(lblTituloSite1);

        gSite1.SetRow(imgbtnSite1, 0);
        gSite1.SetRow(lblTituloSite1, 1);

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
            FontSize = 25
        };

        gPrincipal.Children.Add(lblTitulo);
        gPrincipal.SetRow(lblTitulo, 0);
    }
}