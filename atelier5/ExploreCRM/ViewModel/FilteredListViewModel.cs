using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Objects;

namespace ExploreCRM.ViewModel
{
    public class FilteredListViewModel : INotifyPropertyChanged
    {
        private int _selectedFilter=0;
        private readonly string[] _filters;
        private Model.dataEntities _context;

        public FilteredListViewModel(Model.dataEntities context)
        {
            _context = context;
            _filters = "Tout le staff,10$ et moins,Anniversaires,Commandes françaises,Prix moyens,Commandes".Split(',');
        }
        public IEnumerable<object> FilteredList
        {
            get
            {
                switch(this._selectedFilter)
                {
                    case 0:
                        return from employee in _context.Employees
                               select new
                               {
                                    Prénom = employee.First_Name,
                                    Nom = employee.Last_Name,
                               };
                    case 1:
                        return from product in _context.Products
                               where product.Unit_Price<10.0m
                               select new { Produit = product.Product_Name, Prix = product.Unit_Price };
                    case 2:
                        return from employee in _context.Employees
                               where employee.Birth_Date.Value.Month == 1
                               let age = DateTime.Now.Year-employee.Birth_Date.Value.Year
                               orderby age
                               select new
                               {
                                    Prénom = employee.First_Name,
                                    Nom = employee.Last_Name,
                                    Anniv = employee.Birth_Date.Value.Day,
                                    Age = age
                               };
                    case 3:
                        return from customer in _context.Customers
                               where customer.Country == "France"
                               select new { customer.Company_Name, Nb_Commandes = customer.Orders.Count };
                    case 4:
                        return from category in _context.Categories
                               select new {
                                   Catégorie = category.Category_Name,
                                   Prix_Moyen = category.Products.Average(product => product.Unit_Price)
                               };
                    case 5:
                        return _context.Orders;
                    default:
                        return new string[] { "(Not implemented filter)" };
                }
            }
        }
        public IEnumerable<String> Filters
        {
            get { return _filters; }
        }
        public int SelectedFilter
        {
            get { return this._selectedFilter; }
            set
            {
                this._selectedFilter = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("FilteredList"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
