using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public AsyncDataSource Source = new();

	public MainWindow()
	{
		InitializeComponent();
	}

	private async void Button_Click(object sender, RoutedEventArgs e)
	{
		Output1.Text = "Start";
		for (int i = 0; i < 100; i++)
		{
			await Task.Delay(25);
			Output2.Text += i + "\n";
		}
		Output3.Text = "Ende";

		//Stopwatch sw = Stopwatch.StartNew();
		//Task t1 = Task.Run(Toast); //WICHTIG: Kein Task.Run() notwendig, weil async Task Methoden automatisch starten
		//Task t2 = Task.Run(Tasse);
		//t2.Wait(); //Warte hier auf t2
		//Task t3 = Task.Run(Kaffee);
		//Task.WaitAll(t1, t3); //WhenAll: await für mehrere Tasks
		//Output.Text += sw.ElapsedMilliseconds;
	}

	//private async void Button_Click(object sender, RoutedEventArgs e)
	//{
	//	Stopwatch sw = Stopwatch.StartNew();
	//	Task t1 = ToastAsync(); //WICHTIG: Kein Task.Run() notwendig, weil async Task Methoden automatisch starten
	//	Task t2 = TasseAsync();
	//	await t2; //Warte hier auf t2
	//	Task t3 = KaffeeAsync();
	//	await Task.WhenAll(t1, t3); //WhenAll: await für mehrere Tasks
	//	Output.Text += sw.ElapsedMilliseconds;
	//}

	void Toast()
	{
		Thread.Sleep(4000);
		Dispatcher.Invoke(() => Output2.Text += "Toast fertig\n");
	}

	void Tasse()
	{
		Thread.Sleep(1500);
		Dispatcher.Invoke(() => Output2.Text += "Tasse fertig\n");
	}

	void Kaffee()
	{
		Thread.Sleep(1500);
		Dispatcher.Invoke(() => Output2.Text += "Kaffee fertig\n");
	}

	async Task ToastAsync()
	{
		await Task.Delay(4000); //await Task.Run(() => Thread.Sleep(4000));
		Output2.Text += "Toast fertig\n";
	}

	async Task TasseAsync()
	{
		await Task.Delay(1500);
		Output2.Text += "Tasse fertig\n";
	}

	async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Output2.Text += "Kaffee fertig\n";
	}

	private async void Button_Click_1(object sender, RoutedEventArgs e)
	{
		await foreach (int i in Source.GeneriereZahlen())
		{
			Output2.Text += i + "\n";
		}
	}
}