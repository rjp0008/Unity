package main

import (
  "net"

  "fmt"
)

func main() {

  num := 0
  for i := 0; i < 100; i++ {
    for j := 0; j < 100; j++ {
      num++
      con, _ := net.Dial("udp", "127.0.0.1:2000")
      fmt.Println(num)
      buf := []byte("bla bla bla I am the packet")
      _, err := con.Write(buf)
      if err != nil {
        fmt.Println(err)
      }
    }
  }
}