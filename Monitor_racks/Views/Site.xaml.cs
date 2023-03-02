using Monitor_racks.Models;

namespace Monitor_racks.Views;

[QueryProperty(nameof(ID), "ID")]
public partial class Site : ContentPage
{
    public string ID { get; set; }

    private double fWidth = 0;
    private double fHeight = 0;
    private List<Label> lstlblRack1;
    private List<Image> lstimgRack1;
    private List<Label> lstlblRack2;
    private List<Image> lstimgRack2;
    private List<Label> lstlblRack3;
    private List<Image> lstimgRack3;
    private List<Label> lstlblRack4;
    private List<Image> lstimgRack4;
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
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (fWidth != width || fHeight != height)
        {
            fWidth = width;
            fHeight = height;

            ActualizarVista();

            lstlblRack1 = (fWidth > fHeight)
                ? new List<Label> { lblRackHumedad1H, lblRackTemperatura1H, lblRackPuertas1H, lblRackLuz1H, lblRackElectricidad1H }
                : new List<Label> { lblRackHumedad1V, lblRackTemperatura1V, lblRackPuertas1V, lblRackLuz1V, lblRackElectricidad1V };
            lstimgRack1 = (fWidth > fHeight)
                ? new List<Image> { imgRackHumedad1H, imgRackTemperatura1H, imgRackPuertas1H, imgRackLuz1H, imgRackElectricidad1H }
                : new List<Image> { imgRackHumedad1V, imgRackTemperatura1V, imgRackPuertas1V, imgRackLuz1V, imgRackElectricidad1V };
            lstlblRack2 = (fWidth > fHeight)
                ? new List<Label> { lblRackHumedad2H, lblRackTemperatura2H, lblRackPuertas2H, lblRackLuz2H, lblRackElectricidad2H }
                : new List<Label> { lblRackHumedad2V, lblRackTemperatura2V, lblRackPuertas2V, lblRackLuz2V, lblRackElectricidad2V };
            lstimgRack2 = (fWidth > fHeight)
                ? new List<Image> { imgRackHumedad2H, imgRackTemperatura2H, imgRackPuertas2H, imgRackLuz2H, imgRackElectricidad2H }
                : new List<Image> { imgRackHumedad2V, imgRackTemperatura2V, imgRackPuertas2V, imgRackLuz2V, imgRackElectricidad2V };
            lstlblRack3 = (fWidth > fHeight)
                ? new List<Label> { lblRackHumedad3H, lblRackTemperatura3H, lblRackPuertas3H, lblRackLuz3H, lblRackElectricidad3H }
                : new List<Label> { lblRackHumedad3V, lblRackTemperatura3V, lblRackPuertas3V, lblRackLuz3V, lblRackElectricidad3V };
            lstimgRack3 = (fWidth > fHeight)
                ? new List<Image> { imgRackHumedad3H, imgRackTemperatura3H, imgRackPuertas3H, imgRackLuz3H, imgRackElectricidad3H }
                : new List<Image> { imgRackHumedad3V, imgRackTemperatura3V, imgRackPuertas3V, imgRackLuz3V, imgRackElectricidad3V };
            lstlblRack4 = (fWidth > fHeight)
                ? new List<Label> { lblRackHumedad4H, lblRackTemperatura4H, lblRackPuertas4H, lblRackLuz4H, lblRackElectricidad4H }
                : new List<Label> { lblRackHumedad4V, lblRackTemperatura4V, lblRackPuertas4V, lblRackLuz4V, lblRackElectricidad4V };
            lstimgRack4 = (fWidth > fHeight)
                ? new List<Image> { imgRackHumedad4H, imgRackTemperatura4H, imgRackPuertas4H, imgRackLuz4H, imgRackElectricidad4H }
                : new List<Image> { imgRackHumedad4V, imgRackTemperatura4V, imgRackPuertas4V, imgRackLuz4V, imgRackElectricidad4V };

            clsRack.EmpezarEscuchar($"Site{ID}", "Rack1", lstlblRack1, lstimgRack1, false);
            clsRack.EmpezarEscuchar($"Site{ID}", "Rack1", lstlblRack2, lstimgRack2, false);
            clsRack.EmpezarEscuchar($"Site{ID}", "Rack1", lstlblRack3, lstimgRack3, false);
            clsRack.EmpezarEscuchar($"Site{ID}", "Rack1", lstlblRack4, lstimgRack4, false);

            if (fWidth > fHeight)
            {
                for (int i = 0; i < 5; i++)
                {
                    lstimgRack1[i].HeightRequest = 50;
                    lstimgRack2[i].HeightRequest = 50;
                    lstimgRack3[i].HeightRequest = 50;
                    lstimgRack4[i].HeightRequest = 50;
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    lstimgRack1[i].HeightRequest = 40;
                    lstimgRack2[i].HeightRequest = 40;
                    lstimgRack3[i].HeightRequest = 40;
                    lstimgRack4[i].HeightRequest = 40;
                }
            }
        }
    }

    private async void ActualizarVista()
    {
        if (fWidth > fHeight)
        {
            cvHorizontal.IsVisible = true;
            cvVertical.IsVisible = false;           
        }
        else
        {
            cvHorizontal.IsVisible = false;
            cvVertical.IsVisible = true; 
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"Rack?ID={ID}&IDR=1");
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"Rack?ID={ID}&IDR=2");
    }

    private async void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"Rack?ID={ID}&IDR=3");
    }

    private async void TapGestureRecognizer_Tapped_3(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"Rack?ID={ID}&IDR=4");
    }
}