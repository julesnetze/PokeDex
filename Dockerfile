FROM mcr.microsoft.com/dotnet/sdk:5.0

COPY . .

WORKDIR /./Pokedex

ENV DOTNET_URLS=http://+:5000

EXPOSE 8000:5000

RUN dotnet dev-certs https --trust

RUN dotnet build Pokedex

CMD dotnet run --no-restore --project Pokedex




