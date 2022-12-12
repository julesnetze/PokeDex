FROM mcr.microsoft.com/dotnet/sdk:5.0

COPY . .

WORKDIR /./Pokedex

ENV DOTNET_URLS=https://+:5001

EXPOSE 5001

RUN dotnet dev-certs https --trust

RUN dotnet build Pokedex

CMD dotnet run --no-restore --project Pokedex




