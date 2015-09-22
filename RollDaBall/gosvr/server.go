package main

import "net"
import "fmt"
import "bufio"
import "strings"

func main() {
	fmt.Println("Launching server...") // listen on all interfaces

	ln, _ := net.Listen("tcp", ":8081") // accept connection on port

	for {
		conn, _ := ln.Accept()
		go func(c net.Conn) {
			reader := bufio.NewReader(c)
			for { // will listen for message to process ending in newline (\n)
				message, _ := reader.ReadString('\n') // output message received
				fmt.Println("Message Received:", string(message)) // sample process for string received
				newmessage := strings.ToUpper(message)            // send new string back to client
				c.Write([]byte(newmessage + "\n"))
			}
		}(conn)
	}
}
