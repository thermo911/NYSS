using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Parser.Entities;
using Parser.Exceptions;
using Parser.Services;

namespace Parser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string WorkingFilePath = "data.xlsx";
        private const string FileUri = @"https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        

        private DataLoader _dataLoader = new DataLoader(WorkingFilePath, FileUri);
        private XslxParser _parser = new XslxParser(WorkingFilePath);

        private List<CyberDangerInfo> _infos = new List<CyberDangerInfo>();

        // Paging
        private int _maxItemsPerPage = 15;
        private int _pagesCount = 1;
        private int _curPageNumber = 0;

        private List<CyberDangerInfo> Infos
        {
            get => _infos;
            set
            {
                _infos = value;
                _pagesCount = _infos.Count / _maxItemsPerPage;

                if (_infos.Count % _maxItemsPerPage > 0 || _pagesCount == 0)
                    _pagesCount++;

                CurPageNumber = 0;
                ListBox.ItemsSource = GetPageList();

                UpdateBtn.IsEnabled = true;
            }
        }

        private int CurPageNumber
        {
            get => _curPageNumber;
            set => _curPageNumber = (_pagesCount + value) % _pagesCount;
        }

        public MainWindow()
        {
            InitializeComponent();
            UpdateBtn.IsEnabled = false;
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_dataLoader.FileExists())
            {
                try
                {
                    Infos = _parser.ParseXslx();
                    ListBox.ItemsSource = GetPageList();
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Формат текущего файла некорректный. " +
                        "Нажмите Загрузить, чтобы загрузить новый файл.",
                        "Ошибка при обработке файла",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else
            {
                string caption = "Файл не загружен.";
                string message = "Файл с данными не загружен. Загрузить?";
                MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    MenuItem_DownloadFile_Click(sender, e);
                }
            }
        }

        private void MenuItem_DownloadFile_Click(object sender, RoutedEventArgs e)
        {
            string title = this.Title;
            this.Title += " [Загрузка данных...]";

            try
            {
                _dataLoader.LoadFile();
                Infos = _parser.ParseXslx();
            }
            catch (LoadingException loadExc)
            {
                MessageBox.Show(
                    loadExc.Message,
                    "Ошибка загрузки",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (ParsingException parseExc)
            {
                MessageBox.Show(
                    parseExc.Message,
                    "Ошибка парсинга файла",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            this.Title = title;
        }

        private void ListBox_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var info = ListBox.SelectedItem as CyberDangerInfo;
            DangerInfoWindow infoWindow = new DangerInfoWindow(info);
            infoWindow.Show();
        }

        private List<CyberDangerInfo> GetPageList()
        {
            int first = CurPageNumber * _maxItemsPerPage;
            int last = Math.Min(first + _maxItemsPerPage, Infos.Count);

            List<CyberDangerInfo> page = new List<CyberDangerInfo>(_maxItemsPerPage);
            for (int i = first; i < last; i++)
                page.Add(Infos[i]);

            return page;
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            CurPageNumber++;
            ListBox.ItemsSource = GetPageList();
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            CurPageNumber--;
            ListBox.ItemsSource = GetPageList();
        }

        private void MenuItem_Update_Click(object sender, RoutedEventArgs e)
        {
            File.Delete(WorkingFilePath);
            _dataLoader.LoadFile();
            List<CyberDangerInfo> newInfos = _parser.ParseXslx();

            List<CyberDangerInfo> diff = newInfos.Except(Infos).ToList();
            Infos = newInfos;

            if (diff.Count > 0)
            {
                var diffWindow = new DiffWindow(diff);
                diffWindow.Show();
            }
            else
            {
                MessageBox.Show("Записи совпадают с источником!");
            }
        }
    }
}
