resource "aws_eks_access_policy_association" "eks_access_policy" {
  cluster_name  = aws_eks_cluster.snack_hub.name
  policy_arn    = var.policyArn
  principal_arn = "arn:aws:iam::${var.account_id}:role/voclabs"

  access_scope {
    type = "cluster"
  }
}