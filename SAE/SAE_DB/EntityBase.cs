using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DB
{
    public abstract class INamedEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            var other = obj as INamedEntity;
            return Name == other?.Name && Description == other.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public interface IEntityWithByteId
    {
        public byte Id { get; set; }
    }

    public interface IEntityWithUintId
    {
        public uint Id { get; set; }
    }

    public interface IEntityWithUlongId
    {
        public ulong Id { get; set; }
    }

    public abstract class NamedEntityWithByteId : INamedEntity, IEntityWithByteId
    {
        public byte Id { get; set; }
        public override bool Equals(object? obj)
        {
            var other = obj as NamedEntityWithByteId;
            return Name == other?.Name && Id == other.Id;
        }
    }

    public abstract class NamedEntityWithUintId : INamedEntity, IEntityWithUintId
    {
        public uint Id { get; set; }
        public override bool Equals(object? obj)
        {
            var other = obj as NamedEntityWithByteId;
            return Name == other?.Name && Id == other.Id;
        }
    }

    public abstract class NamedEntityWithUlongId : INamedEntity, IEntityWithUlongId
    {
        public ulong Id { get; set; }
        public override bool Equals(object? obj)
        {
            var other = obj as NamedEntityWithByteId;
            return Name == other?.Name && Id == other.Id;
        }
    }
}
