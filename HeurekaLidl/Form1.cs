using System.Diagnostics;

namespace HeurekaLidl
{
    public partial class Form1 : Form
    {
        // seznam eshopù bez ceny, protože ceny se fetch
        List<Eshop> eshops = new List<Eshop>()
        {
            new Eshop() {name="Alza",responseTime=500 },
            new Eshop() {name="Czc",responseTime=700 },
            new Eshop() {name="Mironet",responseTime=100 },
            new Eshop() {name="Euronics",responseTime=800 },
        };

        Stopwatch stopwatch;

        public Form1()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();

            listBox1.Items.Clear();
            button1.Enabled = false;
            ResultFetcher fetcher = new ResultFetcher();

            EshopFetchData cenaAlza = await fetcher.GetPriceFromAlza("1f5ef87");
            listBox1.Items.Add($"{cenaAlza.name} - {cenaAlza.price} Kè");

            EshopFetchData cenaCzc = await fetcher.GetPriceFromCzc("1f5ef87");
            listBox1.Items.Add($"{cenaCzc.name} - {cenaCzc.price} Kè");

            EshopFetchData cenaMironet = await fetcher.GetPriceFromMironet("1f5ef87");
            listBox1.Items.Add($"{cenaMironet.name} - {cenaMironet.price} Kè");

            button1.Enabled = true;

            stopwatch.Stop();
            MessageBox.Show(stopwatch.ElapsedMilliseconds + " ms");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();

            listBox1.Items.Clear();
            button2.Enabled = false;
            ResultFetcher fetcher = new ResultFetcher();

            List<Task<EshopFetchData>> tasks = new List<Task<EshopFetchData>>();
            tasks.Add(fetcher.GetPriceFromAlza("1f5ef87"));
            tasks.Add(fetcher.GetPriceFromCzc("1f5ef87"));
            tasks.Add(fetcher.GetPriceFromMironet("1f5ef87"));

            EshopFetchData[] results = await Task.WhenAll(tasks);

            for (int i = 0; i < results.Length; i++)
            {
                listBox1.Items.Add($"{results[i].name} - {results[i].price}");
            }

            button2.Enabled = true;

            stopwatch.Stop();
            MessageBox.Show(stopwatch.ElapsedMilliseconds + " ms");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            button3.Enabled = false;
            ResultFetcher fetcher = new ResultFetcher();

            // vytvoøení taskù, které nic nevrací
            List<Task> tasks = new List<Task>();
            foreach(var eshop in eshops)
            {
                // pøedávání eshopu, ze kterého potøebuji data a do kterého se mají data uložit
                tasks.Add(fetcher.FetchData(eshop));
            }

            // oèekávání bez návratové hodnoty, protože hodnoty jsou uložené pøímo u eshopu
            await Task.WhenAll(tasks);

            // vypsání dat z eshopù do listboxu, protože price už je known
            foreach (var eshop in eshops)
            {
                listBox1.Items.Add($"{eshop.name} - {eshop.price}");
            }
        }
    }
}
