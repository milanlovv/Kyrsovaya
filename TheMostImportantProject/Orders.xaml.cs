using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheMostImportantProject
{
    public partial class Orders : Window
    {
        private int id;
        public Orders(int gainId)
        {
            id = gainId;
            InitializeComponent();
        }
        private void Label_MouseUpGoToMenu(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MenuScreen menuScreen = new MenuScreen();
            menuScreen.Show();
            this.Close();
        }

        private void Label_MouseUpGoToBasket(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Basket basket = new Basket();
            basket.Show();
            this.Close();
        }

        private void Label_MouseUpGoToProfile(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillStackPanel();
        }

        public void FillStackPanel()
        {
            using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities()) 
            {
                List<Order> userOrders = db.Order.Where(x => x.ClientID == id).ToList();
                userOrders.Reverse();
                if (!(userOrders == null))
                {
                    foreach (var order in userOrders)
                    {
                        Border border = new Border();
                        DockPanel dockPanel = new DockPanel();
                        Label labelMain = new Label();
                        StackPanel panelLeft = new StackPanel();
                        StackPanel buttonStackPanel = new StackPanel();

                        DockPanel.SetDock(labelMain, Dock.Top);
                        DockPanel.SetDock(panelLeft, Dock.Left);

                        border.Width = 1570;
                        border.Margin = new Thickness(0, 25, 0, 25);
                        border.BorderThickness = new Thickness(2);
                        border.BorderBrush = Brushes.Pink;


                        panelLeft.Orientation = Orientation.Vertical;
                        panelLeft.Margin = new Thickness(30, 38, 0, 0);

                        dockPanel.LastChildFill = false;
                        dockPanel.Width = 1570;

                        labelMain.FontSize = 60;
                        labelMain.Margin = new Thickness(30, 25, 0, 0);

                        
                        foreach (var positions in order.PositionOrder)
                        {
                            StackPanel nameQuantPanel = new StackPanel();
                            Label nameLabel = new Label();
                            Label quantLabel = new Label();

                            nameQuantPanel.Orientation = Orientation.Horizontal;

                            nameLabel.FontSize = 32;
                            quantLabel.FontSize = 32;

                            nameLabel.Content = positions.Position.Name;
                            quantLabel.Content = " - " + positions.Amount + " Шт";

                            nameQuantPanel.Children.Add(nameLabel);
                            nameQuantPanel.Children.Add(quantLabel);

                            nameQuantPanel.Margin = new Thickness(0, 0, 15, 0);

                            panelLeft.Children.Add(nameQuantPanel);
                        }

                        

                        dockPanel.Children.Add(labelMain);
                        dockPanel.Children.Add(panelLeft);
                        dockPanel.Children.Add(buttonStackPanel);

                        border.Child = dockPanel;

                        MainPlace.Children.Add(border);
                    }
                }
            }
        }

    }
}
