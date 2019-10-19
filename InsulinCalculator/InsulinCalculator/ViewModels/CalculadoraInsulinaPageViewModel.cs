using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsulinCalculator.ViewModels
{
    public class CalculadoraInsulinaPageViewModel : ViewModelBase
    {
        public CalculadoraInsulinaPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "Calculadora de Insulina";

            CalcularInsulinaCommand = new DelegateCommand(CalcularInsulina);
        }

        private double _quantidadeCarboidratos = 0;
        private double _relacaoInsulina = 0;
        private double _glicemia = 0;
        private double _insluinaRefeicao = 0;
        private double _insluinaCorrecao = 0;
        private double _insulinaTotal = 0;

        public double QuantidadeCarboidratos 
        {
            get => _quantidadeCarboidratos; 
            set { SetProperty(ref _quantidadeCarboidratos, value); }
        }
        public double RelacaoInsulina 
        {
            get => _relacaoInsulina;
            set { SetProperty(ref _relacaoInsulina, value); }
        }

        public double Glicemia 
        {
            get => _glicemia;
            set { SetProperty(ref _glicemia, value); }
        }

        public double InsulinaCorrecao
        {
            get => _insluinaCorrecao;
            set { SetProperty(ref _insluinaCorrecao, value); }
        }

        public double InsulinaRefeicao
        {
            get => _insluinaRefeicao;
            set { SetProperty(ref _insluinaRefeicao, value); }
        }

        public double InsulinaTotal
        {
            get => _insulinaTotal;
            set { SetProperty(ref _insulinaTotal, value); }
        }

        public DelegateCommand CalcularInsulinaCommand { get; private set; }

        public void CalcularInsulina()
        {
            CalculaInsulinaCorrecao();
            CalculaInsulinaRefeicao();

            if (InsulinaCorrecao >= 0)
                InsulinaTotal = InsulinaRefeicao + InsulinaCorrecao;

        }

        private void CalculaInsulinaCorrecao()
        {
            InsulinaCorrecao = Math.Ceiling((Glicemia - 200) / 80);
            if (InsulinaCorrecao < 0)
                InsulinaCorrecao = 0;
        }

        private void CalculaInsulinaRefeicao()
        {
            InsulinaRefeicao = 0;
            if (RelacaoInsulina > 0)
                InsulinaRefeicao = Math.Ceiling((QuantidadeCarboidratos / RelacaoInsulina));
        }
    }
}
