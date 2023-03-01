using Monitor_racks.Views;
namespace Monitor_racks;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("Principal", typeof(Principal));
        Routing.RegisterRoute("Site", typeof(Site));
        Routing.RegisterRoute("Rack", typeof(Rack));
        Routing.RegisterRoute("RackHorizontal", typeof(RackHorizontal));
        Routing.RegisterRoute("SiteHorizontal", typeof(SiteHorizontal));
        Routing.RegisterRoute("Servicios", typeof(Servicios));
        Routing.RegisterRoute("ServiciosHorizontal", typeof(ServiciosHorizontal));
    }
}
