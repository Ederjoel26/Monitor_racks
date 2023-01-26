using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;

namespace Monitor_racks.Views;

public partial class Login : ContentPage
{
    private double fWidth = 0;
    private double fHeight = 0;
    private Entry eUsuario = new Entry();
    private Entry eContra = new Entry();

    public Login()
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

    private void ActualizarVista()
    {
        gLogin.RowDefinitions.Clear();
        gLogin.ColumnDefinitions.Clear();
        gLogin.Children.Clear();

        if (fWidth > fHeight)
        {
            ActualizarLayout(5, 90, 5, 20, 60, 20);
        }
        else
        {
            ActualizarLayout(25, 50, 25, 10, 80, 10);
        }
    }


    private void ActualizarLayout(int fRow, int sRow, int tRow, int fCol, int sCol, int tCol)
    {
        gLogin.AddRowDefinition(new RowDefinition { Height = new GridLength(fRow, GridUnitType.Star) });
        gLogin.AddRowDefinition(new RowDefinition { Height = new GridLength(sRow, GridUnitType.Star) });
        gLogin.AddRowDefinition(new RowDefinition { Height = new GridLength(tRow, GridUnitType.Star) });
        gLogin.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(fCol, GridUnitType.Star) });
        gLogin.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(sCol, GridUnitType.Star) });
        gLogin.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(tCol, GridUnitType.Star) });

        Frame fContainer = new Frame()
        {
            BackgroundColor = Color.Parse("white"),
            CornerRadius = 5, 
            BorderColor = Color.Parse("black")
        };

        Grid gContainer = new Grid();
        gContainer.AddRowDefinition(new RowDefinition { Height = new GridLength(20, GridUnitType.Star) });
        gContainer.AddRowDefinition(new RowDefinition { Height = new GridLength(20, GridUnitType.Star) });
        gContainer.AddRowDefinition(new RowDefinition { Height = new GridLength(20, GridUnitType.Star) });
        gContainer.AddRowDefinition(new RowDefinition { Height = new GridLength(20, GridUnitType.Star) });
        gContainer.AddRowDefinition(new RowDefinition { Height = new GridLength(20, GridUnitType.Star) });

        Label lblTitulo = new Label
        {
            Text = "Inicio de sesion",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 25,
            TextColor = Color.Parse("black")
        };

        eUsuario = new Entry
        {
            Placeholder = "Usuario",
            VerticalOptions = LayoutOptions.Center,
        };

        eContra = new Entry
        {
            Placeholder = "Contraseña",
            IsPassword = true,
            VerticalOptions = LayoutOptions.Center,
        };

        Button btnEntrar = new Button
        {
            Text = "Entrar",
            CornerRadius = 5,
            Padding = 2,
            VerticalOptions = LayoutOptions.Center,
            Margin = 2
        };

        Button btnHuella = new Button
        {
            Text = "Entrar con huella",
            Padding = 2,
            VerticalOptions = LayoutOptions.Center,
            CornerRadius = 5,
            Margin = 2
        };

        btnEntrar.Clicked += (sender, e) => Button_Clicked(sender, e);
        btnHuella.Clicked += (sender, e) => Button_Clicked_1(sender, e);

        gContainer.Children.Add(lblTitulo);
        gContainer.Children.Add(eUsuario);
        gContainer.Children.Add(eContra);
        gContainer.Children.Add(btnEntrar);
        gContainer.Children.Add(btnHuella);

        gContainer.SetRow(lblTitulo, 0);
        gContainer.SetRow(eUsuario, 1);
        gContainer.SetRow(eContra, 2);
        gContainer.SetRow(btnEntrar, 3);
        gContainer.SetRow(btnHuella, 4);

        fContainer.Content = gContainer;

        gLogin.Children.Add(fContainer);

        gLogin.SetRow(fContainer, 1);
        gLogin.SetColumn(fContainer, 1);
    }

    private async void checkAvaliable()
    {
        var Disponible = await CrossFingerprint.Current.IsAvailableAsync();

        if (!Disponible)
        {
            await DisplayAlert("Error", "La huella dactilar no esta disponible en este momento.", "OK");
            return;
        }

        var request = new AuthenticationRequestConfiguration("Favor de usar la huella dactilar.", "De lo contrario no tendrás acceso.");
        var result = await CrossFingerprint.Current.AuthenticateAsync(request);
        if (result.Authenticated)
        {
            await Shell.Current.GoToAsync("Principal");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        const string Usuario = "test";
        const string Contra = "test";
        if (eUsuario.Text == Usuario && eContra.Text == Contra)
        {
            await Shell.Current.GoToAsync("Principal");
        }
        else
        {
            await DisplayAlert("Usuario o contraseña incorrectos", "Favor de rectificar sus datos.", "Ok");
        }
        eUsuario.Text = string.Empty;
        eContra.Text = string.Empty;
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        checkAvaliable();
    }
}