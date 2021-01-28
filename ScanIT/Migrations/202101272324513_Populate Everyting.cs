namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateEveryting : DbMigration
    {
        public override void Up()
        {
            Sql(@"



                INSERT [dbo].[Categories] ([CATEGORYNAME], [VAT]) 
                VALUES 
                ('Fresh Fruits', '0.13'),
                ('Fresh Meat', '0.24'),
                ('Fresh Vegetables', '0.13'),
                ('Canned Food', '0.24'),
                ('Cleaning Products', '0.24'),
                ('Wine', '0.24'),
                ('Laundry', '0.24'),
                ('Beer', '0.24')

                INSERT [dbo].[Dietaries] ([DietaryName]) 
                VALUES 
                ('Vegan'),
                ('Vegetarian'),
                ('Gluten Free'),
                ('Lactose Free'),
                ('Avoiding Nuts')

                DECLARE @TEMP VARBINARY(MAX)
                SET @TEMP = 1

                INSERT [dbO].[Products] ([ProductName],[Description],[AvailableQuantity],[Photo],[InitialPrice],[BarCode],[QRCode],[CategoryId]) 
                VALUES 
                ('Tangerines', 'Tangerines600g', 10, @TEMP, 1.66, @TEMP, null, 1),
                ('Angus Beef', 'Waitrose Aberdeen Angus Beef Lean Mince 5% Fat400g', 10, @TEMP, 2.66, @TEMP, null, 2),
                ('Carrots', 'Essential Carrots 1kg', 10, @TEMP, 0.50, @TEMP, null, 3),
                ('Baked Beans', 'Essential Baked Beans in Tomato Sauce400g', 10, @TEMP, 0.30, @TEMP, null, 4),
                ('Surface Wipes', 'Dettol AntiBacterial Surface Wipes72s', 10, @TEMP, 2.00, @TEMP, null, 5),
                ('Merlot Wine', 'Luis Felipe Edwards Bin Series Merlot75cl', 10, @TEMP, 5.99, @TEMP, null, 6),
                ('Fabric Conditioner', 'Lenor Super Concentrate Spring Awakening1.19litre', 10, @TEMP, 2.00, @TEMP, null, 7),
                ('Pale Ale', 'Fuller''s E S B London500ml', 10, @TEMP, 5.67, @TEMP, null, 8),
                ('Whole Chicken', 'Waitrose Whole British Chicken with Garlic and Herbs1.5kg', 10, @TEMP, 12.00, @TEMP, null, 2),
                ('Potatoes', 'Duchy Potatoes1.5kg', 10, @TEMP, 1.88, @TEMP, null, 3)

                DECLARE @IMGSTRING VARCHAR(max)
                DECLARE @INSERTSTRING1 VARCHAR(max)
                DECLARE @INSERTSTRING2 VARCHAR(max)
                DECLARE @BARIMGSTRING VARCHAR(max)
                DECLARE @COUNT INT 
                SET @COUNT  = 1

                WHILE @COUNT < 11

                BEGIN 

                SET @IMGSTRING = 'C:\ScanITPictures\ImagesForGroceries\image' + CONVERT(varchar, @COUNT) + '.png'
                SET @BARIMGSTRING = 'C:\ScanITPictures\ImagesForGroceries\Barcode\image' + CONVERT(varchar, @COUNT) + '.png'

                SET @insertString1 = N'UPDATE Products
                                      SET Photo= BulkColumn
                                      FROM OPENROWSET(BULK N''' + @IMGSTRING + ''', SINGLE_BLOB) as ProductImage
                                      WHERE Id = '+ CONVERT(varchar, @COUNT)
                      

                SET @insertString2 = N'UPDATE Products
                                      SET BarCode= BulkColumn
                                      FROM OPENROWSET(BULK N''' + @BARIMGSTRING + ''', SINGLE_BLOB) as BarCodeImage
                                      WHERE Id = '+ CONVERT(varchar, @COUNT)
                      
                EXEC(@insertString1)
                EXEC(@insertString2)

                SET @COUNT = @COUNT + 1
                END
                GO
            ");

            Sql(@"
                    INSERT INTO GENDERS ( [GENDERNAME]) VALUES ('Cis Woman')
                    INSERT INTO GENDERS ( [GENDERNAME]) VALUES ('Cis Man')
                    INSERT INTO GENDERS ( [GENDERNAME]) VALUES ('Trans Woman')
                    INSERT INTO GENDERS ( [GENDERNAME]) VALUES ('Trans Man')
                    INSERT INTO GENDERS ( [GENDERNAME]) VALUES ('Gender Fluid')
                    INSERT INTO GENDERS ( [GENDERNAME]) VALUES ('Other')
                ");

            Sql(@"

                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (1,1)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (1,2)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (1,3)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (1,4)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (1,5)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (2,3)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (2,4)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (2,5)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (3,1)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (3,2)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (3,3)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (3,4)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (3,5)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (4,1)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (4,2)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (4,3)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (4,4)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (4,5)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (6,3)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (6,4)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (6,5)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (8,4)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (8,5)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (9,3)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (9,4)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (9,5)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (10,1)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (10,2)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (10,3)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (10,4)
                INSERT INTO [PRODUCTDIETARIES] ([PRODUCTID],[DIETARYID])
                VALUES (10,5)



            ");

            Sql(@"
                INSERT INTO [PaymentStatus] ([PaymentStatusName])
                VALUES
                ('Paid'),
                ('Pending'),
                ('Overdue')


                INSERT INTO [PaymentMethods] ([PaymentMethodName])
                VALUES
                ('Cash'),
                ('Bank Transfer'),
                ('Debit Card'),
                ('Creadit Card'),
                ('Paypal')

                INSERT INTO [OrderStatus] ([OrderStatusName])
                VALUES
                ('Ongoing'),
                ('Completed'),
                ('Dispatched')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
