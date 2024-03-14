resource "azurerm_service_plan" "this" {
  name                = var.azurerm_service_plan_name
  resource_group_name = data.azurerm_resource_group.this.name
  location            = data.azurerm_resource_group.this.location
  os_type             = "Linux"
  sku_name            = "B1"
}