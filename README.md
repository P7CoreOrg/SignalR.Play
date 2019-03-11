# SignalR.Play
SignalR with Auth

## Curl To Mint An Access Token  

```
curl -X POST \
  https://p7identityserver4.azurewebsites.net/connect/token \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  -H 'Postman-Token: d3c47383-de38-441e-89cb-c195a83f78de' \
  -H 'cache-control: no-cache' \
  -d 'grant_type=arbitrary_resource_owner&client_id=arbitrary-resource-owner-client&client_secret=secret&scope=offline_access%20wizard&arbitrary_claims=%7B%22top%22%3A%5B%22TopDog%22%5D%2C%22role%22%3A%20%5B%22application%22%2C%22limited%22%5D%2C%22query%22%3A%20%5B%22dashboard%22%2C%20%22licensing%22%5D%2C%22seatId%22%3A%20%5B%228c59ec41-54f3-460b-a04e-520fc5b9973d%22%5D%2C%22piid%22%3A%20%5B%222368d213-d06c-4c2a-a099-11c34adc3579%22%5D%7D&subject=PorkyPig&access_token_lifetime=3600&arbitrary_amrs=%5B%22agent%3Ausername%3Aagent0%40supporttech.com%22%2C%22agent%3Achallenge%3AfullSSN%22%2C%22agent%3Achallenge%3AhomeZip%22%5D&custom_payload=%7B%22some_string%22%3A%20%22data%22%2C%22some_number%22%3A%201234%2C%22some_object%22%3A%20%7B%22some_string%22%3A%20%22data%22%2C%22some_number%22%3A%201234%7D%2C%22some_array%22%3A%20%5B%7B%22a%22%3A%20%22b%22%7D%2C%7B%22b%22%3A%20%22c%22%7D%5D%7D&undefined='
```
## Produces  
```
{
    "access_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IkZENkFGOTIyQTAyNTM4NzE5RjhBQjVBRTM0NjdCMjA1MEU2QUExMkUiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJfV3I1SXFBbE9IR2Zpcld1TkdleUJRNXFvUzQifQ.eyJuYmYiOjE1NTIzMzcxMTgsImV4cCI6MTU1MjM0MDcxOCwiaXNzIjoiaHR0cHM6Ly9wN2lkZW50aXR5c2VydmVyNC5henVyZXdlYnNpdGVzLm5ldCIsImF1ZCI6WyJodHRwczovL3A3aWRlbnRpdHlzZXJ2ZXI0LmF6dXJld2Vic2l0ZXMubmV0L3Jlc291cmNlcyIsIndpemFyZCJdLCJjbGllbnRfaWQiOiJhcmJpdHJhcnktcmVzb3VyY2Utb3duZXItY2xpZW50Iiwic3ViIjoiUG9ya3lQaWciLCJhdXRoX3RpbWUiOjE1NTIzMzcxMTgsImlkcCI6ImxvY2FsIiwidG9wIjoiVG9wRG9nIiwicm9sZSI6WyJhcHBsaWNhdGlvbiIsImxpbWl0ZWQiXSwicXVlcnkiOlsiZGFzaGJvYXJkIiwibGljZW5zaW5nIl0sInNlYXRJZCI6IjhjNTllYzQxLTU0ZjMtNDYwYi1hMDRlLTUyMGZjNWI5OTczZCIsInBpaWQiOiIyMzY4ZDIxMy1kMDZjLTRjMmEtYTA5OS0xMWMzNGFkYzM1NzkiLCJjbGllbnRfbmFtZXNwYWNlIjoiRGFmZnkgRHVjayIsInNjb3BlIjpbIndpemFyZCIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJhcmJpdHJhcnlfcmVzb3VyY2Vfb3duZXIiLCJhZ2VudDp1c2VybmFtZTphZ2VudDBAc3VwcG9ydHRlY2guY29tIiwiYWdlbnQ6Y2hhbGxlbmdlOmZ1bGxTU04iLCJhZ2VudDpjaGFsbGVuZ2U6aG9tZVppcCJdLCJjdXN0b21fcGF5bG9hZCI6eyJzb21lX3N0cmluZyI6ImRhdGEiLCJzb21lX251bWJlciI6MTIzNCwic29tZV9vYmplY3QiOnsic29tZV9zdHJpbmciOiJkYXRhIiwic29tZV9udW1iZXIiOjEyMzR9LCJzb21lX2FycmF5IjpbeyJhIjoiYiJ9LHsiYiI6ImMifV19fQ.eD9YHRratgi9L443AE6SrKB0COyTP9d-3mFmzCSrONuWeSLaDphiCMctI8q5dOgjeT8Xfkc47FhvQqqHeB_ipZZMSDP--GvbIjkAIY_RddfFbpVHaYwQQwq8OhD4ynH9xorK6konxoDCYh_3sUpW1eBreTooiwB2-6tvf1R1GolVaOLN6OUMAQ_mL3RSXa_8m7e4I6VMgSw1t2CYa_0FDq-IQjXHTN2942aADT3qCjj6y6xla3UrBc1A1Ez_x8Z4iVF4SdBcrOyn3TYJx0jPHv8Y6genJzbjsvRpr12eY8xyfelq-fSgFu1P56szN0yUwqrDI4s8VlO3SRFmw_U36A",
    "expires_in": 3600,
    "token_type": "Bearer",
    "refresh_token": "CfDJ8CMRKZ66wHFJlbKcOZ8BCocH-GgKe5iOSNPZSX-Sk5wLUou8KsKgbC-5ckp0AlxTIL9zkLmglhfJBDk5fZa0No0WP6f9ieaC8quWy2L1QfmBcwM-Aod2HLDvyY-eVEkSgvUyDoz1D2UI0sY-i6v-ovVvbJE15p4ZHUhIGm0mRJEfKfe0xj1ElTHRMEetjCUn-sACrW4CKf5Wwj9bAKbX4P0"
}
```

# SignalRChatClient  
Add the access_token that you mint into the client.

