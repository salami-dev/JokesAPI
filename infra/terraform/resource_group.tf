data "azurerm_resource_group" "this" {
  name = var.resource_group_name
}

output "resource_group_id" {
  value = data.azurerm_resource_group.this.id
}
