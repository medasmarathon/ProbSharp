echo "Adding migration: $1"
dotnet ef migrations add $1 --startup-project ProbSharp --project ProbSharp.Persistence