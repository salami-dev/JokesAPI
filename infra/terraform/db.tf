resource "azurerm_postgresql_flexible_server" "this" {
  name                   = var.postgres_server_name
  resource_group_name    = data.azurerm_resource_group.this.name
  location               = data.azurerm_resource_group.this.location
  version                = "13"
  create_mode = "Default"

  administrator_login    = var.postgres_administrator_login
  administrator_password = var.postgres_administrator_password

  storage_mb   = 32768
  
  zone = 1

  sku_name   = "B_Standard_B1ms"

}

resource "azurerm_postgresql_flexible_server_database" "this" {
  name      = var.postgres_database_name
  server_id = azurerm_postgresql_flexible_server.this.id
  collation = "en_US.utf8"
  charset   = "utf8"

  # prevent the possibility of accidental data loss
  lifecycle {
    prevent_destroy = true
  }
}

resource "azurerm_postgresql_flexible_server_firewall_rule" "this" {
  count               = length(azurerm_linux_web_app.this.outbound_ip_address_list)

  name                = format("rule-%d", count.index)
  server_id           = azurerm_postgresql_flexible_server.this.id
  start_ip_address    = azurerm_linux_web_app.this.outbound_ip_address_list[count.index]
  end_ip_address      = azurerm_linux_web_app.this.outbound_ip_address_list[count.index]
}
