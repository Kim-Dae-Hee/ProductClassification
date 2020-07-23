using ProductClassification.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProductClassification
{
    public partial class Monitoring : Form
    {
        private int oldCount = 0;
        public Monitoring()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, System.EventArgs e)
        {
            if (DesignMode)
                return;

            ShowChart();

            Timer timer = new Timer();
            timer.Interval = 1000 * 10; // 10초
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
           
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int newCount = DataRepository.Product.GetCount();
            if (newCount != oldCount)
                ShowChart();
        }

        private void ShowChart()
        {
            var allProducts = DataRepository.Product.GetAll();

            List<ProductReader> NotDefectiveProducts = GetProductReaders(allProducts, false);
            List<ProductReader> DefectiveProducts = GetProductReaders(allProducts, true);

            //MessageBox.Show($"{NotDefectiveProducts.Count} / {DefectiveProducts.Count}");
            NotDefectiveProducts.AddRange(DefectiveProducts);

            oldCount = allProducts.Count;

            bdsProduct.DataSource = NotDefectiveProducts;
        }

        private List<ProductReader> GetProductReaders(List<Product> allProducts, bool check)
        {
            List<ProductReader> productReaders = new List<ProductReader>();
            List<string> dates = new List<string>();

            foreach (Product product in allProducts)
            {
                //MessageBox.Show($"{product.DateOnly} / {product.IsDefective} / {product.QRCodeData}");
                if (product.IsDefective != check)
                    continue;

                if (!dates.Contains(product.DateOnly))
                {
                    dates.Add(product.DateOnly);
                    ProductReader productReader = new ProductReader();
                    productReader.DateOnly = product.DateOnly;
                    productReader.Count += 1;
                    if(check == false)
                        productReader.IsDefective = "정상";
                    else
                        productReader.IsDefective = "오류";

                    productReaders.Add(productReader);
                }
                else
                {
                    string dateOnly = dates.Find(x => x == product.DateOnly);
                    int index = productReaders.FindIndex(x => x.DateOnly == dateOnly);
                    if (index != -1)
                        productReaders[index].Count += 1;
                }
            }

            return productReaders;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (MessageBox.Show("모니터링을 종료하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}

