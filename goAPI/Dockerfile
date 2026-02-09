FROM golang:alpine AS builder
WORKDIR /build
COPY . ./
RUN go mod download
RUN mkdir bin
RUN go build -o bin/api main.go

FROM golang:alpine
WORKDIR /app
COPY --from=builder /build/bin/api .
CMD ["./api"]