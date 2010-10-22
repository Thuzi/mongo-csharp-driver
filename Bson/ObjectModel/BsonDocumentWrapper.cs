﻿/* Copyright 2010 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace MongoDB.Bson {
    // this class is a wrapper for an object that we intend to serialize as a BSON document
    // it is a subclass of BsonValue so that it may be used where a BsonValue is expected
    // this class is mostly used by MongoCollection and MongoCursor when supporting generic query objects

    public class BsonDocumentWrapper : BsonValue, IBsonSerializable {
        #region private fields
        private object document;
        #endregion

        #region constructors
        public BsonDocumentWrapper(
            object document
        )
            : base(BsonType.Document) {
            this.document = document;
        }
        #endregion

        #region public static methods
        public static new BsonDocumentWrapper Create(
            object document
        ) {
            if (document != null) {
                return new BsonDocumentWrapper(document);
            } else {
                return null;
            }
        }
        #endregion

        #region public methods
        public override int CompareTo(
            BsonValue other
        ) {
            throw new InvalidOperationException("CompareTo not supported for BsonDocumentWrapper");
        }

        public object DeserializeDocument(
            BsonReader bsonReader,
            Type nominalType
        ) {
            throw new InvalidOperationException("DeserializeDocument not valid for BsonDocumentWrapper");
        }

        public object DeserializeElement(
            BsonReader bsonReader,
            Type nominalType,
            out string name
        ) {
            throw new InvalidOperationException("DeserializeElement not valid for BsonDocumentWrapper");
        }

        public bool DocumentHasIdProperty() {
            return false;
        }

        public bool DocumentHasIdValue(
            out object existingId
        ) {
            throw new InvalidOperationException();
        }

        public override bool Equals(
            object obj
        ) {
            throw new InvalidOperationException("Equals not supported for BsonDocumentWrapper");
        }

        public void GenerateDocumentId() {
            throw new InvalidOperationException();
        }

        public override int GetHashCode() {
            throw new InvalidOperationException("GetHashCode not supported for BsonDocumentWrapper");
        }

        public void SerializeDocument(
            BsonWriter bsonWriter,
            Type nominalType,
            bool serializeIdFirst
        ) {
            BsonSerializer.SerializeDocument(bsonWriter, document, serializeIdFirst);
        }

        public void SerializeElement(
            BsonWriter bsonWriter,
            Type nominalType,
            string name,
            bool useCompactRepresentation
        ) {
            BsonSerializer.SerializeElement(bsonWriter, name, document, useCompactRepresentation);
        }

        public override string ToString() {
            return this.ToJson();
        }
        #endregion
    }
}
