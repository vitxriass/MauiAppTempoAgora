using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Net;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao =
                            $"Clima: {t.main} - {t.description}\n" +
                            $"Latitude: {t.lat}\n" +
                            $"Longitude: {t.lon}\n" +
                            $"Nascer do Sol: {t.sunrise}\n" +
                            $"Pôr do Sol: {t.sunset}\n" +
                            $"Temp. Máxima: {t.temp_max}°C\n" +
                            $"Temp. Mínima: {t.temp_min}°C\n" +
                            $"Vento: {t.speed} m/s\n" +
                            $"Visibilidade: {t.visibility} metros";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        await DisplayAlert("Cidade não encontrada", "Verifique o nome digitado e tente novamente.", "OK");
                        lbl_res.Text = "";
                    }
                }
                else
                {
                    await DisplayAlert("Aviso", "Digite o nome de uma cidade.", "OK");
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Erro de Conexão", "Verifique sua conexão com a internet.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Algo deu errado: {ex.Message}", "OK");
            }
        }
    }
}
