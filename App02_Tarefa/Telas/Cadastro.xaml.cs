using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App02_Tarefa.Modelos;

namespace App02_Tarefa.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage
    {
        private byte Prioridade { get; set; }

        public Cadastro()
        {
            InitializeComponent();
        }
        public void PrioridadeSelectAction(object sender, EventArgs args)
        {
            var Stacks = SlPrioridades.Children;

            foreach (var Linha in Stacks)
            {
                Label LblPrioridade = ((StackLayout)Linha).Children[1] as Label;
                LblPrioridade.TextColor = Color.Gray;
                LblPrioridade.BackgroundColor= Color.Transparent;


            }

           
            FileImageSource Source = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;
            string Prioridade = Source.File.ToString().Replace("p", "").Replace(".ng","");
           
            this.Prioridade = byte.Parse(Prioridade);
            if (this.Prioridade == 1)
            {
                ((Label)((StackLayout)sender).Children[1]).TextColor = Color.White;
                ((Label)((StackLayout)sender).Children[1]).BackgroundColor = Color.Green;
            }
            if (this.Prioridade == 2)
            {
                ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
                ((Label)((StackLayout)sender).Children[1]).BackgroundColor = Color.Yellow;
            }
            if (this.Prioridade == 3)
            {
                ((Label)((StackLayout)sender).Children[1]).TextColor = Color.White;
                ((Label)((StackLayout)sender).Children[1]).BackgroundColor = Color.Orange;
            }
            if (this.Prioridade == 4)
            {
                ((Label)((StackLayout)sender).Children[1]).TextColor = Color.White;
                ((Label)((StackLayout)sender).Children[1]).BackgroundColor = Color.Red;
                
            }
        }
        public void SalvarAction(object sender, EventArgs args)
        {
            bool ErroExiste = false;
            if (TxtNome.Text == null || TxtNome.Text.Trim().Length <= 0)
            {
                DisplayAlert("ERRO", "Nome não preenchido", "OK");
                ErroExiste = true;
            }
            if (Prioridade <= 0)
            {
                DisplayAlert("ERRO", "Prioridade não foi informada", "OK");
                ErroExiste = true;
            }
            if(ErroExiste == false)
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Nome = TxtNome.Text.Trim();
                tarefa.Prioridade = this.Prioridade;

                new GerenciadorTarefa().Salvar(tarefa);
                App.Current.MainPage = new NavigationPage(new Inicio());
                
                
            }
        }
    }
}