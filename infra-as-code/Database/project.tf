resource "mongodbatlas_project" "snack_hub" {
  name   = var.project_name
  org_id = var.org_id
}

output "snack_hub_name" {
  value = mongodbatlas_project.snack_hub.name
}