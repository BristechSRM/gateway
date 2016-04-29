﻿namespace Models

open System
open System.Net

type ErrorResponse =
    { HttpStatusCode : HttpStatusCode
      Body : string }

type Result<'Success, 'Failure> =
    | Success of 'Success
    | Failure of 'Failure

type SpeakerSummary =
    { Id : Guid
      Forename : string
      Surname : string
      Rating : int
      ImageUri : string }

type AdminSummary =
    { Id : Guid
      Forename : string
      Surname : string
      ImageUri : string }

type LastContactSummary =
    { Date : DateTime
      SenderId : Guid
      ReceiverId : Guid }

type SessionSummary =
    { Id : Guid
      Title : string
      Status : string
      Date : DateTime option
      Speaker : SpeakerSummary
      Admin : AdminSummary option
      LastContact : LastContactSummary option }

type SessionDetail =
    { Id : Guid
      Title : string
      Status : string
      Date : DateTime option
      DateAdded : string
      Speaker : SpeakerSummary
      Admin : AdminSummary option
      LastContact : LastContactSummary option
      ThreadId : Guid }

type EventSession =
    { Id : Guid
      Title : string
      Description : string
      SpeakerId : Guid
      SpeakerForename : string
      SpeakerSurname : string
      SpeakerBio : string
      SpeakerImageUri : string
      SpeakerRating : int
      StartDate : DateTime option
      EndDate : DateTime option }

type EventSummary =
    { Id : string
      Date : DateTime
      Description : string
      Location : string
      Sessions : Guid[] }

type EventDetail =
    { Id : string
      Date : DateTime
      Description : string
      Location : string
      Sessions : EventSession[] }