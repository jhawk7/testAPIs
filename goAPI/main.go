package main

import "github.com/gin-gonic/gin"

func main() {
	router := gin.Default()
	router.GET("/health", func(c *gin.Context) {
		c.JSON(200, gin.H{
			"message": "ok",
		})
	})

	router.GET("/broken", func(c *gin.Context) {
		c.JSON(500, gin.H{
			"message": "broken",
		})
	})
	router.Run(":8383")
}
