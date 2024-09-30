resource "mongodbatlas_database_user" "default_user" {
  username           = var.dbuser
  password           = var.dbuser_password
  project_id         = mongodbatlas_project.snack_hub.id
  auth_database_name = "admin"

  roles {
    role_name     = "readWrite"
    database_name = var.database_name
  }
  labels {
    key   = "Name"
    value = "Database User"
  }
}
output "default_user" {
  value = mongodbatlas_database_user.default_user.username
}