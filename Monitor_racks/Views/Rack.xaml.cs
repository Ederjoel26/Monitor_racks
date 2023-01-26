using Monitor_racks.Models;

namespace Monitor_racks.Views;
[QueryProperty(nameof(ID), "ID")]
[QueryProperty(nameof(IDR), "IDR")]
public partial class Rack : ContentPage
{
	public string ID { get; set; }
	public string IDR { get; set; }

    private double fWidth = 0;
    private double fHeight = 0;

    private Label lblPRack = new Label(), lblERack = new Label(), lblLRack = new Label(), lblHRack = new Label(), lblTRack = new Label();
    private Image imgPRack = new Image(), imgERack = new Image(), imgLRack = new Image(), imgHRack = new Image(), imgTRack = new Image();

    private Grid gRack = new Grid();
    public Rack()
	{
		InitializeComponent();

        fWidth = Width;
        fHeight = Height;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Title = $"Site {ID} Rack {IDR}";

        List<Label> lstlblRack = new List<Label> { lblPRack, lblERack, lblLRack, lblHRack, lblTRack };
        List<Image> lstimgRack = new List<Image> { imgPRack, imgERack, imgLRack, imgHRack, imgTRack };

		clsRack.EmpezarEscuchar($"Site{ID}", $"Rack{IDR}", lstlblRack, lstimgRack);
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
        gRack?.RowDefinitions.Clear();
        gRack?.ColumnDefinitions.Clear();
        gRack?.Children.Clear();

        if (fWidth > fHeight)
        {
            await Shell.Current.GoToAsync($"Principal", false);
            await Shell.Current.GoToAsync($"RackHorizontal?ID={ ID }&IDR={ IDR }");
        }
        else
        {
            gRack?.Add(ActualizarAPortraitLayout(), 0, 1);
        }
        Content = default;
        Content = gRack;
    }

    private Grid ActualizarAPortraitLayout()
    {
        InicializarTitulo();

        Grid gValoresRack = new Grid
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
                new ColumnDefinition{ Width = new GridLength(10, GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(80, GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(10, GridUnitType.Star) },
            }
        };

        Frame fHumedad = ValoresRack(imgHRack, lblHRack);
        Frame fTemperatura = ValoresRack(imgTRack, lblTRack);
        Frame fPuertas = ValoresRack(imgPRack, lblPRack);
        Frame fLuz = ValoresRack(imgLRack, lblLRack);
        Frame fElectricidad = ValoresRack(imgERack, lblERack);

        gValoresRack.Add(fHumedad, 1, 0);
        gValoresRack.Add(fTemperatura, 1, 1);
        gValoresRack.Add(fPuertas, 1, 2);
        gValoresRack.Add(fLuz, 1, 3);
        gValoresRack.Add(fElectricidad, 1, 4);

        return gValoresRack;
    }

    private Frame ValoresRack(Image imgValor, Label lblValor)
    {
        Grid gValores = new Grid
        {
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

        Frame fValores = new Frame
        {
            Content = gValores,
            CornerRadius = 10,
            BackgroundColor = Color.Parse("white"),
            Margin = 3,
            BorderColor = Color.Parse("black")
        };

        return fValores;
    }

    private void InicializarTitulo()
    {
        Label lblTitulo = new Label
        {
            FontAttributes = FontAttributes.Bold,
            Text = $"Site {ID} Rack{IDR}",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 25
        };

        gRack.Add(lblTitulo, 0, 0);
    }
}