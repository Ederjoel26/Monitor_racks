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

	private Label Servicio1 = new Label(), Servicio2 = new Label(), Servicio3 = new Label(), Servicio4 = new Label(), Servicio5 = new Label();
	private Label EstatusServicio1 = new Label(), EstatusServicio2 = new Label(), EstatusServicio3 = new Label(), EstatusServicio4 = new Label(), EstatusServicio5 = new Label();

	private int Id = 0;

	private Grid gServicios = new Grid();
	public Servicios()
	{
		InitializeComponent();

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

	private void verificarServicio(int Id, string IPAddress, Label lblServicio, Label lblEstatus)
	{
		lblServicio.Text = IPAddress;
		try
		{
			Ping ping = new Ping();
			PingReply Replicar = ping.Send(IPAddress, 1000);
			if(Replicar != null)
			{
				lblEstatus.Text =	$"Estatus: { Replicar.Status } \n " +
									$"Time: { Replicar.RoundtripTime } \n " +
									$"Address: { Replicar.Address }";

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