#!/bin/bash
dotnet ef dbcontext scaffold \
  "Server=localhost;Database=PaperDb;User Id=yehor;Password=pass1234;" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir Models \
  --context-dir . \
  --context MyDbContext  \
  --no-onconfiguring \
  --data-annotations \
  --force