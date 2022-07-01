using App02_Tarefa.Modelos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App02_Tarefa.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            CultureInfo culture = new CultureInfo("pt-BR");
            string data = DateTime.Now.ToString("dddd, dd {0} MMMM {0} yyyy", culture);
            DataHoje.Text = string.Format(data, "de");
            CarregarTarefas();
        }
        public void ActionGoCadastro(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Cadastro());
        }
        private void CarregarTarefas()
        {
            SLTarefas.Children.Clear();
            List<Tarefa> lista = new GerenciadorTarefa().Listagem();

            int i = 0;
            foreach(Tarefa tarefa in lista)
            {
                LinhaStackLayout(tarefa, i);
                i++;
            }
        }
        public void LinhaStackLayout(Tarefa tarefa, int index)
        {
            Image Delete = new Image()
            {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("Delete.png")
            };
           
            TapGestureRecognizer DeleteTap = new TapGestureRecognizer();
            DeleteTap.Tapped += delegate
            {
                new GerenciadorTarefa().Deletar(index);
                CarregarTarefas();
            };

            Delete.GestureRecognizers.Add(DeleteTap);


            Image Prioridade = new Image()
            {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("p"+tarefa.Prioridade+".png") 
            };
            View StackCentral = null;
            if(tarefa.DataFinalizacao == null)
            {
                StackCentral = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = tarefa.Nome
            };

            }
            else
            {
                StackCentral = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Spacing = 0,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    
                };
                ((StackLayout)StackCentral).Children.Add(new Label() { Text = tarefa.Nome, TextColor = Color.Gray });
                ((StackLayout)StackCentral).Children.Add(new Label() { Text = "Finalizado em: "+tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy - hh:mm")+"h", TextColor = Color.Gray, FontSize = 10 });
            }


            
            Image Check = new Image()
            {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("CheckOff.png")
            };

            if(tarefa.DataFinalizacao != null)
            {
                Check.Source = ImageSource.FromFile("CheckOn.png");
            }

            TapGestureRecognizer CheckTap = new TapGestureRecognizer();
            CheckTap.Tapped += delegate
            {
                new GerenciadorTarefa().Finalizar(index,tarefa);
                CarregarTarefas();
            };

            Check.GestureRecognizers.Add(CheckTap);

            StackLayout linha = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 15
            };
            linha.Children.Add(Check);
            linha.Children.Add(StackCentral);
            linha.Children.Add(Prioridade);
            linha.Children.Add(Delete);

            SLTarefas.Children.Add(linha);


        }
    }   
}