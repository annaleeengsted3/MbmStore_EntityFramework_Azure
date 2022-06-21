using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MbmStore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    Published = table.Column<short>(nullable: true),
                    ISBN = table.Column<string>(nullable: true),
                    Director = table.Column<string>(nullable: true),
                    Released = table.Column<short>(nullable: true),
                    Artist = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    MusicCD_Released = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    PhoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    PhoneType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.PhoneId);
                    table.ForeignKey(
                        name: "FK_Phone_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    TrackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Composer = table.Column<string>(nullable: true),
                    Length = table.Column<TimeSpan>(nullable: false),
                    MusicCDProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.TrackId);
                    table.ForeignKey(
                        name: "FK_Track_Product_MusicCDProductId",
                        column: x => x.MusicCDProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItem_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "Address", "Birthdate", "City", "Email", "Firstname", "Lastname", "Zip" },
                values: new object[,]
                {
                    { 1, "Irisdahlsvej 32", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Århus N", null, "Tina", "Petterson", "8200" },
                    { 2, "Solsikkevej 32", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Århus C", null, "Thomas", "Larsson", "8000" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Category", "Discriminator", "ImageUrl", "Price", "Title", "Author", "ISBN", "Published", "Publisher" },
                values: new object[,]
                {
                    { 1, "Book", "Book", "A_Hard_Days_Write.jpg", 150.00m, "A Hard Day's Write: The Stories Behind Every Beatles Song ", "Steve Turner", "978-0060844097", (short)2005, "It Books" },
                    { 2, "Book", "Book", "The Making of Sgt. Pepper.jpg", 180.00m, "With a Little Help from My Friends: The Making of Sgt. Pepper", "Georg Martin", "0316547832", (short)1995, "Little Brown & Co" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Category", "Discriminator", "ImageUrl", "Price", "Title", "Director", "Released" },
                values: new object[,]
                {
                    { 5, "Movie", "Movie", "junglebook.jpg", 160.50m, "Jungle Book", "Jon Favreau", (short)0 },
                    { 6, "Movie", "Movie", "gladiator.jpg", 49.95m, "Gladiator", "Ridley Scott", (short)0 },
                    { 7, "Movie", "Movie", "forrest-gump.jpg", 160.50m, "Forrest Gump", "Robert Zemeckis", (short)0 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Category", "Discriminator", "ImageUrl", "Price", "Title", "Artist", "Label", "MusicCD_Released" },
                values: new object[,]
                {
                    { 3, "Music", "MusicCD", "abbey_road.jpg", 128.00m, "Abbey Road (Remastered)", "Beatles", "EMI", (short)2009 },
                    { 4, "Music", "MusicCD", "revolver.jpg", 128.00m, "Revolver (Remastered)", "The Beatles", "EMI", (short)2009 }
                });

            migrationBuilder.InsertData(
                table: "Track",
                columns: new[] { "TrackId", "Composer", "Length", "MusicCDProductId", "Title" },
                values: new object[,]
                {
                    { 18, "Harrison", new TimeSpan(0, 0, 2, 28, 0), null, "Taxman" },
                    { 19, "Lennon, McCartney", new TimeSpan(0, 0, 2, 6, 0), null, "Eleanor Rigby" },
                    { 20, "Lennon, McCartney", new TimeSpan(0, 0, 3, 0, 0), null, "I'm Only Sleeping" },
                    { 21, "Harrison", new TimeSpan(0, 0, 2, 59, 0), null, "Love You To" },
                    { 22, "Harrison", new TimeSpan(0, 0, 2, 23, 0), null, "Here, There And Everywhere" },
                    { 23, "Lennon, McCartney", new TimeSpan(0, 0, 2, 38, 0), null, "Yellow Submarine" },
                    { 27, "Lennon, McCartney", new TimeSpan(0, 0, 1, 59, 0), null, "For No One" },
                    { 25, "Lennon, McCartney", new TimeSpan(0, 0, 2, 9, 0), null, "Good Day Sunshine" },
                    { 26, "Lennon, McCartney", new TimeSpan(0, 0, 2, 0, 0), null, "And Your Bird Can Sing" },
                    { 17, "Lennon, McCartney", new TimeSpan(0, 0, 0, 23, 0), null, "Her Majesty" },
                    { 28, "Lennon, McCartney", new TimeSpan(0, 0, 1, 14, 0), null, "Doctor Robert" },
                    { 29, "Harrison", new TimeSpan(0, 0, 2, 27, 0), null, "I Want To Tell You" },
                    { 24, "Lennon, McCartney", new TimeSpan(0, 0, 2, 36, 0), null, "She Said She Said" },
                    { 16, "Lennon, McCartney", new TimeSpan(0, 0, 2, 19, 0), null, "The End" },
                    { 11, "Lennon, McCartney", new TimeSpan(0, 0, 1, 6, 0), null, "Mean Mr. Mustard" },
                    { 14, "Lennon, McCartney", new TimeSpan(0, 0, 1, 31, 0), null, "Golden Slumbers" },
                    { 13, "Lennon, McCartney", new TimeSpan(0, 0, 1, 57, 0), null, "She Came In Through The Bathroom Window" },
                    { 12, "Lennon, McCartney", new TimeSpan(0, 0, 1, 12, 0), null, "Polythene Pam" },
                    { 30, "Lennon, McCartney", new TimeSpan(0, 0, 2, 29, 0), null, "Got To Get You Into My Life" },
                    { 10, "Lennon, McCartney", new TimeSpan(0, 0, 2, 26, 0), null, "Sun King" },
                    { 9, "Lennon, McCartney", new TimeSpan(0, 0, 4, 2, 0), null, "You Never Give Me Your Money" },
                    { 8, "Lennon, McCartney", new TimeSpan(0, 0, 2, 45, 0), null, "Because" },
                    { 7, "Harrison", new TimeSpan(0, 0, 3, 5, 0), null, "Here Comes The Sun" },
                    { 6, "Lennon, McCartney", new TimeSpan(0, 0, 7, 47, 0), null, "I Want You (She's So Heavy)" },
                    { 5, "Starkey", new TimeSpan(0, 0, 2, 51, 0), null, "Octopus's Garden" },
                    { 4, "Lennon, McCartney", new TimeSpan(0, 0, 3, 26, 0), null, "Oh! Darling" },
                    { 3, "Lennon, McCartney", new TimeSpan(0, 0, 3, 29, 0), null, "Maxwell's Silver Hammer" },
                    { 2, "Harrison", new TimeSpan(0, 0, 3, 3, 0), null, "Something" },
                    { 1, "Lennon, McCartney", new TimeSpan(0, 0, 4, 20, 0), null, "Come Together" },
                    { 15, "Lennon, McCartney", new TimeSpan(0, 0, 1, 36, 0), null, "Carry That Weight" },
                    { 31, "Lennon, McCartney", new TimeSpan(0, 0, 3, 1, 0), null, "Tomorrow Newer Knows" }
                });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "InvoiceId", "CustomerId", "OrderDate" },
                values: new object[] { 1, 1, new DateTime(2018, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "InvoiceId", "CustomerId", "OrderDate" },
                values: new object[] { 2, 2, new DateTime(2018, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "OrderItemId", "InvoiceId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 7, 1 },
                    { 2, 1, 2, 1 },
                    { 3, 2, 1, 1 },
                    { 4, 2, 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_InvoiceId",
                table: "OrderItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_CustomerId",
                table: "Phone",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Track_MusicCDProductId",
                table: "Track",
                column: "MusicCDProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "Track");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
