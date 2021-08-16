USE [Product]
GO
SET IDENTITY_INSERT [dbo].[LookupCategory] ON 
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (1, N'Cat_5cc19', 1)
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (2, N'Tablet', 1)
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (3, N'Laptop', 1)
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (4, N'RC Toy', 1)
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (5, N'Tişört', 1)
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (6, N'cat22', 0)
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (7, N'Cat_0a665', 1)
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (8, N'Cat_1c2ce', 1)
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (9, N'Cat_94a93', 1)
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (10, N'Cat_318ea', 1)
GO
INSERT [dbo].[LookupCategory] ([Id], [Name], [Status]) VALUES (11, N'Cat_6dd7e', 1)
GO
SET IDENTITY_INSERT [dbo].[LookupCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [Status]) VALUES (1, 1, N'Product_111', CAST(11.00 AS Decimal(9, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [Status]) VALUES (2, 2, N'Tablet245', CAST(500.00 AS Decimal(9, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [Status]) VALUES (3, 3, N'Laptop2500', CAST(1600.00 AS Decimal(9, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [Status]) VALUES (4, 4, N'Car1', CAST(25.00 AS Decimal(9, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [Status]) VALUES (5, 5, N'WhiteTShirt', CAST(5.00 AS Decimal(9, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [Status]) VALUES (10, 1, N'Product_11234', CAST(10.00 AS Decimal(9, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [Status]) VALUES (11, 1, N'Product_9f1e6', CAST(10.00 AS Decimal(9, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [Status]) VALUES (12, 1, N'Product_d953b', CAST(10.00 AS Decimal(9, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [Status]) VALUES (13, 1, N'Product_32adf', CAST(10.00 AS Decimal(9, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [CategoryId], [Name], [Price], [Status]) VALUES (14, 1, N'Product_18c76', CAST(10.00 AS Decimal(9, 2)), 1)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (1, 1)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (1, 2)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (1, 4)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (1, 5)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (2, 1)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (2, 2)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (2, 4)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (2, 5)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (3, 1)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (3, 2)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (3, 4)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (3, 5)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (4, 1)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (4, 2)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (4, 4)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (5, 1)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (5, 2)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (5, 3)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (6, 1)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (6, 2)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (7, 1)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (8, 1)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (9, 1)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (10, 1)
GO
INSERT [dbo].[CategoryAttribute] ([CategoryId], [AttributeId]) VALUES (11, 1)
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (1, 1, N'Small')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (2, 1, N'Big')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (2, 2, N'Gray')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (2, 4, N'Xiaomi')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (2, 5, N'Android')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (3, 1, N'Medium')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (3, 2, N'Yellow')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (3, 4, N'Sony')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (3, 5, N'Windows')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (4, 1, N'Big')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (4, 2, N'Green')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (4, 4, N'Xyz')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (5, 1, N'Medium')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (5, 2, N'White')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (5, 3, N'Male')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (10, 1, N'a4dfc')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (11, 1, N'2979e')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (12, 1, N'de58a')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (13, 1, N'9e1ad')
GO
INSERT [dbo].[ProductAttribute] ([ProductId], [AttributeId], [AttributeValue]) VALUES (14, 1, N'95d4f')
GO
SET IDENTITY_INSERT [dbo].[LookupAttribute] ON 
GO
INSERT [dbo].[LookupAttribute] ([Id], [Name], [Status]) VALUES (1, N'Attr_da7a6', 1)
GO
INSERT [dbo].[LookupAttribute] ([Id], [Name], [Status]) VALUES (2, N'Color', 1)
GO
INSERT [dbo].[LookupAttribute] ([Id], [Name], [Status]) VALUES (3, N'Gender', 1)
GO
INSERT [dbo].[LookupAttribute] ([Id], [Name], [Status]) VALUES (4, N'Brand', 1)
GO
INSERT [dbo].[LookupAttribute] ([Id], [Name], [Status]) VALUES (5, N'OS', 1)
GO
INSERT [dbo].[LookupAttribute] ([Id], [Name], [Status]) VALUES (6, N'newopne2', 0)
GO
INSERT [dbo].[LookupAttribute] ([Id], [Name], [Status]) VALUES (7, N'Attr_c8f54', 1)
GO
INSERT [dbo].[LookupAttribute] ([Id], [Name], [Status]) VALUES (8, N'Attr_b04c2', 1)
GO
INSERT [dbo].[LookupAttribute] ([Id], [Name], [Status]) VALUES (9, N'Attr_f38f1', 1)
GO
SET IDENTITY_INSERT [dbo].[LookupAttribute] OFF
GO
