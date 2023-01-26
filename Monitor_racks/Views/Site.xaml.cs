using Monitor_racks.Models;

namespace Monitor_racks.Views;

[QueryProperty(nameof(ID), "ID")]
public partial class Site : ContentPage
{
    public string ID { get; set; }

    private double fWidth = 0;
    private double fHeight = 0;

    private Label lblPRack1S1 = new Label(), lblERack1S1 = new Label(), lblLRack1S1 = new Label(), lblHRack1S1 = new Label(), lblTRack1S1 = new Label();
    private Label lblPRack2S1 = new Label(), lblERack2S1 = new Label(), lblLRack2S1 = new Label(), lblHRack2S1 = new Label(), lblTRack2S1 = new Label();
    private Label lblPRack3S1 = new Label(), lblERack3S1 = new Label(), lblLRack3S1 = new Label(), lblHRack3S1 = new Label(), lblTRack3S1 = new Label();
    private Label lblPRack4S1 = new Label(), lblERack4S1 = new Label(), lblLRack4S1 = new Label(), lblHRack4S1 = new Label(), lblTRack4S1 = new Label();

    private Image imgPRack1S1 = new Image(), imgERack1S1 = new Image(), imgLRack1S1 = new Image(), imgHRack1S1 = new Image(), imgTRack1S1 = new Image();
    private Image imgPRack2S1 = new Image(), imgERack2S1 = new Image(), imgLRack2S1 = new Image(), imgHRack2S1 = new Image(), imgTRack2S1 = new Image();
    private Image imgPRack3S1 = new Image(), imgERack3S1 = new Image(), imgLRack3S1 = new Image(), imgHRack3S1 = new Image(), imgTRack3S1 = new Image();
    private Image imgPRack4S1 = new Image(), imgERack4S1 = new Image(), imgLRack4S1 = new Image(), imgHRack4S1 = new Image(), imgTRack4S1 = new Image();


    private List<Label> lstRack1 = new List<Label>();
    private List<Label> lstRack2 = new List<Label>();
    private List<Label> lstRack3 = new List<Label>();
    private List<Label> lstRack4 = new List<Label>();

    private List<Image> lstImgRack1 = new List<Image>();
    private List<Image> lstImgRack2 = new List<Image>();
    private List<Image> lstImgRack3 = new List<Image>();
    private List<Image> lstImgRack4 = new List<Image>();

    private Grid gSite = new Grid();
    public Site()
    {
        InitializeComponent();

        fWidth = Width;
        fHeight = Height;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Title = $"Site {ID}";

        lstRack1 = new List<Label>() { lblPRack1S1, lblERack1S1, lblLRack1S1, lblHRack1S1, lblTRack1S1 };
        lstRack2 = new List<Label>() { lblPRack2S1, lblERack2S1, lblLRack2S1, lblHRack2S1, lblTRack2S1 };
        lstRack3 = new List<Label>() { lblPRack3S1, lblERack3S1, lblLRack3S1, lblHRack3S1, lblTRack3S1 };
        lstRack4 = new List<Label>() { lblPRack4S1, lblERack4S1, lblLRack4S1, lblHRack4S1, lblTRack4S1 };

        lstImgRack1 = new List<Image>() { imgPRack1S1, imgERack1S1, imgLRack1S1, imgHRack1S1, imgTRack1S1 };
        lstImgRack2 = new List<Image>() { imgPRack2S1, imgERack2S1, imgLRack2S1, imgHRack2S1, imgTRack2S1 };
        lstImgRack3 = new List<Image>() { imgPRack3S1, imgERack3S1, imgLRack3S1, imgHRack3S1, imgTRack3S1 };
        lstImgRack4 = new List<Image>() { imgPRack4S1, imgERack4S1, imgLRack4S1, imgHRack4S1, imgTRack4S1 };

        clsRack.EmpezarEscuchar($"Site{ID}", "Rack1", lstRack1, lstImgRack1);
        clsRack.EmpezarEscuchar($"Site{ID}", "Rack2", lstRack2, lstImgRack2);
        clsRack.EmpezarEscuchar($"Site{ID}", "Rack3", lstRack3, lstImgRack3);
        clsRack.EmpezarEscuchar($"Site{ID}", "Rack4", lstRack4, lstImgRack4);
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
        gSite?.RowDefinitions.Clear();
        gSite?.ColumnDefinitions.Clear();
        gSite?.Children.Clear();

        if (fWidth > fHeight)
        {
            await Shell.Current.GoToAsync($"Principal", false);
            await Shell.Current.GoToAsync($"SiteHorizontal?ID={ ID }");
        }
        else
        {
            gSite?.Add(ActualizarAPortraitLayout(), 0, 1);
        }
        Content = default;
        Content = gSite;
    }

    private Grid ActualizarAPortraitLayout()
    {
        InicializarTitulo();

        Grid gRacks = new Grid
        {
            Margin = 3,
            RowDefinitions = {
                new RowDefinition { Height = new GridLength(25, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(25, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(25, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(25, GridUnitType.Star) }
            }
        };

        Frame gRack1 = CuadroValores(lstRack1, lstImgRack1);
        Frame gRack2 = CuadroValores(lstRack2, lstImgRack2);
        Frame gRack3 = CuadroValores(lstRack3, lstImgRack3);
        Frame gRack4 = CuadroValores(lstRack4, lstImgRack4);

        TapGestureRecognizer tgrRack1 = new TapGestureRecognizer(); 
        TapGestureRecognizer tgrRack2 = new TapGestureRecognizer();
        TapGestureRecognizer tgrRack3 = new TapGestureRecognizer();
        TapGestureRecognizer tgrRack4 = new TapGestureRecognizer();
        
        tgrRack1.Tapped += (sender, e) => Frame_tapped(sender, e);
        tgrRack2.Tapped += (sender, e) => Frame_tapped_1(sender, e);
        tgrRack3.Tapped += (sender, e) => Frame_tapped_2(sender, e);
        tgrRack4.Tapped += (sender, e) => Frame_tapped_3(sender, e);
        
        gRack1.GestureRecognizers.Add(tgrRack1);
        gRack2.GestureRecognizers.Add(tgrRack2);
        gRack3.GestureRecognizers.Add(tgrRack3);
        gRack4.GestureRecognizers.Add(tgrRack4);

        gRacks.Add(gRack1, 0, 0);
        gRacks.Add(gRack2, 0, 1);
        gRacks.Add(gRack3, 0, 2);
        gRacks.Add(gRack4, 0, 3);

        return gRacks;
    }

    private Frame CuadroValores(List<Label> lstlblRack, List<Image> lstimgRack)
    {
        Grid gRack = new Grid()
        {
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(50, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(50, GridUnitType.Star) },
            },
            ColumnDefinitions =
            {
                new ColumnDefinition{ Width = new GridLength(33, GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(33, GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(33, GridUnitType.Star) },
            }
        };

        Grid gPuertas = ValoresRack(lstimgRack[0], lstlblRack[0]);
        Grid gEnergia = ValoresRack(lstimgRack[1], lstlblRack[1]);
        Grid gLuz = ValoresRack(lstimgRack[2], lstlblRack[2]);
        Grid gHumedad = ValoresRack(lstimgRack[3], lstlblRack[3]);
        Grid gTemperatura = ValoresRack(lstimgRack[4], lstlblRack[4]);

        gRack.Add(gPuertas, 0, 0);
        gRack.Add(gEnergia, 2, 0);
        gRack.Add(gLuz, 0, 1);
        gRack.Add(gHumedad, 1, 1);
        gRack.Add(gTemperatura, 2, 1);

        Frame fRack = new Frame
        {
            Content = gRack,
            CornerRadius = 10,
            BackgroundColor = Color.Parse("white"),
            Margin = 3,
            BorderColor = Color.Parse("black")
        };

        return fRack;
    }

    private async void Frame_tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"Rack?ID={ID}&IDR=1");
    }

    private async void Frame_tapped_1(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"Rack?ID={ID}&IDR=2");
    }

    private async void Frame_tapped_2(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"Rack?ID={ID}&IDR=3");
    }

    private async void Frame_tapped_3(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"Rack?ID={ID}&IDR=4");
    }

    private Grid ValoresRack(Image imgValor, Label lblValor)
    {
        Grid gValores = new Grid
        {
            Margin = 1,
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(70, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(30, GridUnitType.Star) },
            }
        };

        imgValor.HeightRequest = 50;

        lblValor.VerticalOptions = LayoutOptions.Center;
        lblValor.HorizontalOptions = LayoutOptions.Center;

        gValores.Add(imgValor, 0, 0);
        gValores.Add(lblValor, 0, 1);

        return gValores;
    }

    private void InicializarTitulo()
    {
        gSite.AddRowDefinition(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });
        gSite.AddRowDefinition(new RowDefinition { Height = new GridLength(95, GridUnitType.Star) });

        Label lblTitulo = new Label
        {
            FontAttributes = FontAttributes.Bold,
            Text = $"Site { ID }",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 25
        };

        gSite.Add(lblTitulo, 0, 0);
    }
}