﻿module DataTransform
open System

module Handle = 
    let toHandle (dto : Dtos.Handle) : Models.Handle =
        { Type = dto.Type
          Identifier = dto.Identifier }

    let toHandleDto (handle : Models.Handle) : Dtos.Handle = 
        { Type = handle.Type
          Identifier = handle.Identifier }

module Profile = 

    let toAdmin (handles : Dtos.Handle seq) (profile : Dtos.Profile): Models.Admin =
        { Id = profile.Id
          Forename = profile.Forename
          Surname = profile.Surname
          ImageUri = profile.ImageUrl
          Handles = handles |> Seq.map Handle.toHandle }

    let toAdminSummary (profile : Dtos.Profile) : Models.AdminSummary =
        { Id = profile.Id
          Forename = profile.Forename
          Surname = profile.Surname
          ImageUri = profile.ImageUrl }

    let toSpeaker (handles : Dtos.Handle seq) (profile : Dtos.Profile) : Models.Speaker =
        { Id = profile.Id
          Forename = profile.Forename
          Surname = profile.Surname
          Rating = profile.Rating
          ImageUri = profile.ImageUrl
          Bio = profile.Bio
          Handles = handles |> Seq.map Handle.toHandle }

    let toSpeakerSummary (profile : Dtos.Profile): Models.SpeakerSummary = 
        { Id = profile.Id
          Forename = profile.Forename
          Surname = profile.Surname
          Rating = profile.Rating
          ImageUri = profile.ImageUrl }

module Session = 

    let convertToLastContactSummary (dto : Dtos.LastContact) : Models.LastContactSummary =
        { Date = dto.Date; SenderId = dto.ProfileIdOne; ReceiverId = dto.ProfileIdTwo }

    let getLastContact (senderId : Guid) (receiverId : Guid) (lastContacts : Dtos.LastContact[]) =
        lastContacts
        |> Seq.tryFind (fun lastContact -> (lastContact.ProfileIdOne.Equals senderId && lastContact.ProfileIdTwo.Equals receiverId) || (lastContact.ProfileIdOne.Equals receiverId && lastContact.ProfileIdTwo.Equals senderId))
        |> Option.map convertToLastContactSummary

    let toModel lastContacts speaker admin (session : Dtos.Session) : Models.Session =
        { Id = session.Id
          Title = session.Title
          Status = session.Status
          Date = session.Date
          DateAdded = session.DateAdded
          Speaker = speaker
          Admin = admin
          LastContact = 
            match session.AdminId with 
            | Some aid -> getLastContact aid session.SpeakerId lastContacts
            | None -> None }

    let toEventSession (speaker : Models.Speaker) (session : Dtos.Session) : Models.EventSession = 
        { Id = session.Id
          Title = session.Title
          Description = session.Description
          SpeakerId = speaker.Id
          SpeakerForename = speaker.Forename
          SpeakerSurname = speaker.Surname
          SpeakerBio = speaker.Bio
          SpeakerImageUri = speaker.ImageUri
          SpeakerRating = speaker.Rating
          StartDate = session.Date
          EndDate = session.Date |> Option.map (fun date -> date.AddHours(1.0)) }