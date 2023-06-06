﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TheMostImportantProject
{

    public partial class CreatingOrder : Window
    {
        List<string> hours = new List<string>();
        List<string> minutes = new List<string>();
        public CreatingOrder()
        {
            InitializeComponent();

            for (int i = 0; i < 14; i++)
            {
                hours.Add((i+8).ToString());
            }
            for (int i = 0; i < 60; i++)
            {
                minutes.Add(i.ToString());
            }
      
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

        private void Label_MouseUpGoToOrders(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Orders orders = new Orders(CurrentUser.Id);
            orders.Show();
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
            decimal sum = BasketItems.Lines.Sum(x => x.Position.Price * x.Quantity);
            SumLabel.Content = "Стоимость заказа: " + String.Format("{0:0.00}", sum) + " Рублей";
        }


        private void ButtonOrderDone_Click(object sender, RoutedEventArgs e)
        {
           
            decimal sum = BasketItems.Lines.Sum(x => x.Position.Price * x.Quantity);
            
            
        
            
                
                using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
                {
                        Order order = new Order();
                        order.Sum = sum;
                    



                        order.ClientID = CurrentUser.Id;
                        db.Order.Add(order);
                        db.SaveChanges();

                        int orderId = db.Order.Where(x => x.ClientID == CurrentUser.Id).Max(x => x.OrderID);
                        foreach (var line in BasketItems.Lines)
                        {
                            PositionOrder positionOrder = new PositionOrder();
                            positionOrder.OrderID = orderId;
                            positionOrder.PositionID = line.Position.PositionID;
                            positionOrder.Amount = line.Quantity;
                            db.PositionOrder.Add(positionOrder);
                            db.SaveChanges();
                        }
                        BasketItems.Lines.Clear();
                        Orders orders = new Orders(CurrentUser.Id);
                        orders.Show();
                        this.Close();
                    
            }
        }

        
    }
}
