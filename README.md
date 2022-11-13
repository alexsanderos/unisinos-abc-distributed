## Architecture

C4 Model

### Context Diagram

![context](https://www.plantuml.com/plantuml/png/RLBBRjim4BppAxOwAK1jzP1JJqaS8mtWDaPj8yX9eAIDJ8HQ2ijbaw3er_JG3ycFbPILs7deHEtEpCuiUU95iuJEga-6QXiQXAr8xxyM1UlxQMja6whWaMj7WYJJsdL5RpKbpiHBOqrLp8udorJQV5yKdVQ274lbB34UPDedTRKulRdQhClho_MtSZ6_cFyupu-K4YCMOVKOEHsC16YCRbbtSEiOBib7J31F_vx-EZWzcocrHFQEyZfuSHtoPyXM4dsJPFiZM4Dts80C730x5J6EfbaSEu-3k6VZiJnpWHhDZtdz9TBVZSkEuBTI4B-ToVmUgz8Fnu_W8sfmiWl5w7j7tbGnPmhKi_jb6aCjtAAM4Dl9q1-bTlXo_Y1vfutT3raXDHTi6l0lskijGjmKCcaBE4duiGjzl4v27c8UI8xbij9qjvTUwdQ5RM2N3Jy3P72PO22ioUiwRMqm-HFjmTPVBMBZyGLosFXux8X-2tuT-rdo9CQFB-W_ "context")

### Container Diagram

![container](https://www.plantuml.com/plantuml/svg/bLRBRXen5Dtp5IxT42c1Hggww2e9GKrI0fGXod9qP-O25XvxP7iWjEhNTD4FoO_rsZCy9nJ22cpVUywvZoEkZ0LjyrJKFd0PYpn1DhKsCrzRBGsBveJRQHxb1dMif4Lfcx5AMva0wM8kZ4q4ZrhjpnS3T_Jqs6sbO2ngEWhR505SecvcBdyjkEi_XuFECEZtmlxJg7lVwpJEQhK1QgDa8ywDLIdgSrOFBD6GjhxvoWINCqoOLUpwfisuD9RRt78bctLA4Im9CmrlL2uJqCj6_8b2xuj7_FNlwvyYh7XMFsE_QZL6dpN1iFDY6mkCGiWoYdp6Y5rdcSD_W3cOMFFCdhDhESa5Q7PvwMxQWZjY6ascfE6HG3PMccLQpNd2vOJPARAuU0HnZCOGRQhC669qd7S8D5AwXmc6L6hA_LZyOZUvSL3VOJm3zsMaGHASjXwAtb8ivSKvWPXgqa8HNolYArNjZwpN6R4lzTNLuFu_Geml6niZs9nULN6KZgTeh0QhT1YlZYbtV-kYWglq37K5TfDhNKELPW9Vs5YeHHNyDjg5Jw8zN7OB5Y8mwCVfHvS5gEUeY-5I1EyJKsy8FGKtKcyKqdCDCrYcLEaD-K5nSA8qttw8lR05xPLN5u6SCUlwwcwKP1cj8s4I2SUX8hRaKEWiyOzBBADEKwdLc0iCGO9O6cwsv1Otj0Z5pOcwVQAVgtOwBs4bna4EKba-vh4dkadSspez4TidMZfiNtqQ2d8HLs9U3FBljNau2oXh7xgcXCNO-ofF0fpaMjNz3_-aMC2ztlcCGnHRNhOnbIVZ4Ttr-d2pe6qGmf2zEOYxqMWGj1x89hUypT5BWOiDhG1byatpfDnFxmzKNtmXdLHnXXVk_MfxIsdDac8utBZ3hlUuj8Qj5B0yIy1YQJ6EO1b_Ut3hTzCDyaXmCsq5UUHyDC8jjohox2_NU-CFyQZQmX21f8srIcac4xRGtEAsfc65ozi3uLhJybroHN-46MLn5kgRU5ZOpkeUuz9zUnNsUdMqg9LTVbzrXmTOx0pYqN9SqKZHFu1_ "container")

## API Entrypoint events

<ul>
    <li>Technology: C#, .NET 6, Kafka and Masstransit</li>
    <li>Description: Web API responsible for providing system features</li>
</ul>

## Analyze Profile

<ul>
    <li>Technology: C#, .NET 6, Kafka and Masstransit</li>
    <li>Description: Worker responsible for interacting with the bank transaction partner</li>
</ul>

## Orchestrator

<ul>
    <li>Technology: C#, .NET 6, Kafka, Masstransit and SQL Server</li>
    <li>Description: Worker responsible for orchestrating the complex flows</li>
</ul>

### BPMN Course Purchase Flow

![Orchestrator SAGA](./docs/sagas/diagram.svg)

## Notification

<ul>
    <li>Technology: C#, .NET 6, Kafka and Masstransit</li>
    <li>Description: Worker responsible for notifying interested parties</li>
</ul>

## Payment

<ul>
    <li>Technology: C#, .NET 6, Kafka, Masstransit and SQL Server</li>
    <li>Description: Worker responsible for interacting with the bank transaction partner</li>
</ul>
