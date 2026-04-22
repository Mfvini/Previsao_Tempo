using Previsao_Tempo.Models;
using Previsao_Tempo.Services;

namespace Previsao_Tempo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // 1. Verificação de Internet
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert("Sem Conexão", "Você precisa estar conectado à internet para buscar o clima.", "OK");
                    return;
                }

                // 2. Verifica se o campo está preenchido
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        // 3. Monta a exibição dos dados
                        string dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n" +
                                         $"Visibilidade: {t.visibility} \n" +
                                         $"Descrição: {t.description} \n" +
                                         $"Velocidade do Vento: {t.speed} \n";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        // 4. Caso a cidade não exista (API retornou null)
                        lbl_res.Text = "Cidade não encontrada.";
                        await DisplayAlert("Erro", "A cidade informada não foi localizada.", "OK");
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha o nome da cidade.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", $"Erro inesperado: {ex.Message}", "OK");
            }
        } // Fim do método Button_Clicked
    } // Fim da classe MainPage
} // Fim do namespace
