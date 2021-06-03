using GraduationProjectML.Model;
using LatestGraduationProject.Models;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace LatestGraduationProject.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        #region Props
        //This is for the Image path
        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }


        //This is for the itemscontrol
        private List<Data> _labels = new List<Data>();
        public List<Data> Labels
        {
            get { return _labels; }
            set { SetProperty(ref _labels, value); }
        }
        #endregion

        #region Ctors
        public MainWindowViewModel()
        {
            ModelInput testData = new ModelInput();
            testData.ImageSource = @"D:\LatestProject\LatestGraduationProject\LatestGraduationProject\test.png";
            ConsumeModel.Predict(testData);
        }
        #endregion

        #region Commands
        public DelegateCommand AnalyseCommand => new DelegateCommand(ExecuteCommandName);

        #endregion

        #region Methods
        void ExecuteCommandName()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png";
            if ((bool)dialog.ShowDialog())
            {
                ImagePath = dialog.FileName;
                ModelInput modelInput = new ModelInput { ImageSource = dialog.FileName };
                ModelOutput modelOutput = ConsumeModel.Predict(modelInput);
                var labels = File.ReadAllLines(@"D:\LatestProject\LatestGraduationProject\LatestGraduationProject\ClassLabels.txt");
                List<Data> temp = new List<Data>();
                for (int i = 0; i < labels.Length; i++)
                {
                    Data data = new Data
                    {
                        Label = labels[i],
                        Prediction = modelOutput.Score[i],
                        Percentage = modelOutput.Score[i].ToString("P", CultureInfo.InvariantCulture)
                    };
                    temp.Add(data);
                }
                temp.Sort();
                Labels = temp;
            }
        }
        #endregion

    }
}
