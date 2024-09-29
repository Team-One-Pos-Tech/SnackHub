variable "LabRoleName" {
  description = "Name for the LabRole IAM role"
  default     = "LabRole"
}

variable "PrincipalRoleName" {
  description = "Name for the Principal IAM role"
  default     = "voclabs"
}

variable "policyarn" {
  default = "arn:aws:eks::aws:cluster-access-policy/AmazonEKSClusterAdminPolicy"
}